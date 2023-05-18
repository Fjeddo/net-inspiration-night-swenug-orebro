using MailerFunctions.Vertical.Features.WelcomeMail;
using MailerFunctions.Vertical.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Timers;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Xunit;

namespace MailerFunctions.Vertical.Tests.Features.WelcomeMail;

public class Triggers
{
    private readonly Vertical.Features.WelcomeMail.Triggers _sut;
    private readonly IMediator _mediator;

    public Triggers()
    {
        _mediator = Substitute.For<IMediator>();
        _sut = new Vertical.Features.WelcomeMail.Triggers(_mediator, new NullLogger<Vertical.Features.WelcomeMail.Triggers>());
    }

    [Fact]
    public async Task SendWelcomeMail_Should_Dispatch_SendWelcomeEmailCommand()
    {
        await _sut.SendWelcomeMail(new TimerInfo(new ConstantSchedule(TimeSpan.Zero), new ScheduleStatus()));

        _mediator.SentOneOfTypeAndOneInTotal<Vertical.Features.WelcomeMail.Process.Command>();
    }

    [Fact]
    public async Task ManualSendWelcomeMail_Should_Dispatch_SendWelcomeEmailCommand()
    {
        await _sut.ManualSendWelcomeMail(new DefaultHttpRequest(new DefaultHttpContext()));

        _mediator.SentOneOfTypeAndOneInTotal<Vertical.Features.WelcomeMail.Process.Command>();
    }
}

public class Process
{
    public class CommandHandler
    {
        [Fact]
        public async Task Should_Dispatch_SentEvent()
        {
            var mediatr = Substitute.For<IMediator>();
            mediatr
                .Send(Arg.Any<Vertical.Features.WelcomeMail.Process.PendingMailQuery>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(new Vertical.Features.WelcomeMail.Process.PendingMailQuery.Result[] {new() {Address = "123@456.se"}}));

            var sut = new Vertical.Features.WelcomeMail.Process.CommandHandler(mediatr, Substitute.For<IMailHelper>(), NullLogger<Vertical.Features.WelcomeMail.Process.CommandHandler>.Instance);

            await sut.Handle(default, default);

            mediatr.PublishOneOfTypeAndOneInTotal<Vertical.Features.WelcomeMail.Process.SentEvent>();
        }
    }

    public class PendingMailQueryHandler
    {
        [Fact]
        public async Task Should_Do_XYZ()
        {

        }
    }

    public class SentEventHandler
    {
        [Fact]
        public async Task Should_Do_XYZ()
        {

        }
    }
}
