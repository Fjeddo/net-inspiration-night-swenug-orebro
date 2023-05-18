using System;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MailerFunctions.Vertical.Data;
using MailerFunctions.Vertical.Infrastructure;
using MailerFunctions.Vertical.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace MailerFunctions.Vertical.Features.WelcomeMail;

public class Triggers
{
    private readonly ISender _mediatr;
    private readonly ILogger<Triggers> _log;

    public Triggers(ISender mediatr, ILogger<Triggers> log)
    {
        _mediatr = mediatr;
        _log = log;
    }

    [FunctionName(nameof(ManualSendWelcomeMail))]
    public async Task<IActionResult> ManualSendWelcomeMail([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        await Execute();
        return new OkResult();
    }

    [FunctionName(nameof(SendWelcomeMail))]
    public async Task SendWelcomeMail([TimerTrigger(SendWelcomeMailNCrontab, RunOnStartup = false)] TimerInfo timerInfo)
    {
        await Execute();
    }

    private async Task Execute()
    {
        _log.LogInformation("Creating and dispatching a {command}", nameof(Process.Command));
        var command = new Process.Command();
        await _mediatr.Send(command);
    }

#if DEBUG
    private const string SendWelcomeMailNCrontab = Constants.NeverRunExpression;
#else
    private const string SendWelcomeMailNCrontab = Constants.OnceEveryHourAtTheHour;
#endif

    public class Constants
    {
        public const string NeverRunExpression = "0 0 0 31 2 0";
        public const string OnceEveryHourAtTheHour = "0 0 * * * *";
    }
}

internal static class Process
{
    public class Command : IRequest
    {
        /* No data to be carried with the command */
    }

    public class CommandHandler : IRequestHandler<Command>
    {
        private readonly IMediator _mediatr;
        private readonly IMailHelper _mailHelper;
        private readonly ILogger<CommandHandler> _log;

        public CommandHandler(IMediator mediatr, IMailHelper mailHelper, ILogger<CommandHandler> log)
        {
            _mediatr = mediatr;
            _mailHelper = mailHelper;
            _log = log;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            _log.LogInformation("Handling {command}", nameof(Command));

            var pendingEmailsQuery = new PendingMailQuery();
            var pendingEmails = await _mediatr.Send(pendingEmailsQuery, cancellationToken);

            foreach (var pendingWelcomeEmail in pendingEmails)
            {
                _mailHelper.SendMailMessage(CreateWelcomeMail(pendingWelcomeEmail.Address, pendingWelcomeEmail.Recipient));
                await _mediatr.Publish(new SentEvent {Address = pendingWelcomeEmail.Address});
            }
        }

        private static MailMessage CreateWelcomeMail(string to, string name)
            => new(
                "noreply@tomtens-hus.fi",
                to,
                "Välkommen!", $"Vi är glada att just du, {name}, har registrerat dig hos oss!");
    }

    public class PendingMailQuery : IRequest<PendingMailQuery.Result[]>
    {
        internal class Result
        {
            public string Address { get; set; }
            public string Recipient { get; set; }
        }
    }

    public class PendingMailQueryHandler : IRequestHandler<PendingMailQuery, PendingMailQuery.Result[]>
    {
        private readonly Db _db;
        private readonly ILogger<PendingMailQueryHandler> _log;

        public PendingMailQueryHandler(Db db, ILogger<PendingMailQueryHandler> log)
        {
            _db = db;
            _log = log;
        }

        public Task<PendingMailQuery.Result[]> Handle(PendingMailQuery request, CancellationToken cancellationToken)
        {
            _log.LogInformation("Handling {query}", nameof(PendingMailQuery));

            return Task.FromResult(
                _db.Mail.AsQueryable()
                    .Where(x => x.Type == MailType.Welcome)
                    .Where(x => !x.CreatedAt.HasValue)
                    .Select(x => new PendingMailQuery.Result {Address = x.Member.Address, Recipient = x.Member.Name})
                    .ToArray());
        }
    }

    public class SentEvent : INotification
    {
        public string Address { get; set; }
    }

    public class SentEventHandler : INotificationHandler<SentEvent>
    {
        private readonly Db _db;
        private readonly ILogger<SentEventHandler> _log;

        public SentEventHandler(Db db, ILogger<SentEventHandler> log)
        {
            _db = db;
            _log = log;
        }

        public Task Handle(SentEvent notification, CancellationToken cancellationToken)
        {
            _log.LogInformation("Handling {event}, sent email to {address}", nameof(SentEvent), notification.Address);

            _db.Mail.Where(x => x.Member.Address == notification.Address).ToList().ForEach(x => x.CreatedAt = DateTimeOffset.Now);

            return Task.CompletedTask;
        }
    }
}
