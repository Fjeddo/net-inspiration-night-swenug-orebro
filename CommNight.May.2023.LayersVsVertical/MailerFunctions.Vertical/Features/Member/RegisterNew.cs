using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MailerFunctions.Vertical.Data;
using MailerFunctions.Vertical.Infrastructure;
using MailerFunctions.Vertical.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace MailerFunctions.Vertical.Features.Member;

internal class Triggers
{
    private readonly ISender _mediatr;

    public Triggers(ISender mediatr)
    {
        _mediatr = mediatr;
    }

    [FunctionName(nameof(ManualRegisterNewMember))]
    public async Task<IActionResult> ManualRegisterNewMember([HttpTrigger(AuthorizationLevel.Function, "post")] RegistrationRequest req)
    {
        await ExecuteRegisterNewMember(req);
        return new OkResult();
    }

    //[FunctionName(nameof(RegisterNewMember))]
    //public async Task RegisterNewMember([ServiceBusTrigger("member-registrations")] RegistrationRequest message)
    //{
    //    await ExecuteRegisterNewMember(message);
    //}

    public class RegistrationRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    private async Task ExecuteRegisterNewMember(RegistrationRequest request) => await _mediatr.Send(new Process.Command(request.Name, request.Email));
}

internal static class Process
{
    public record Command(string Name, string Address) : IRequest;

    public class CommandValidator : IPipelineBehavior<Command, Unit>
    {
        private readonly Db _db;

        public CommandValidator(Db db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(Command request, RequestHandlerDelegate<Unit> next, CancellationToken cancellationToken)
        {
            if (_db.Members.Any(x => x.Address == request.Address))
            {
                throw new Exception($"Member with address {request.Address} already exists");
            }

            return await next();
        }
    }

    public class CommandHandler : IRequestHandler<Command>
    {
        private readonly Db _db;
        private readonly ILogger<CommandHandler> _log;

        public CommandHandler(Db db, ILogger<CommandHandler> log)
        {
            _db = db;
            _log = log;
        }

        public Task Handle(Command request, CancellationToken cancellationToken)
        {
            _log.LogRequestHandling();

            var member = new Models.Member(request.Name, request.Address);
            _db.Members.Add(member);
            _db.Mail.Add(new Mail(MailType.Welcome, member));

            return Task.CompletedTask;
        }
    }
}
