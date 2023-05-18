using MediatR;
using NSubstitute;

namespace MailerFunctions.Vertical.Tests;

public static class MediatorTestExtensions
{
    public static void SentXOfTypeAndYInTotal<T>(this IMediator mediator, int xOfType, int yInTotal)
        where T : IRequest =>
        Task.WaitAll(
            mediator.Received(xOfType).Send(Arg.Any<T>()),
            mediator.ReceivedWithAnyArgs(yInTotal).Send(Arg.Any<IRequest>()));

    public static void SentOneOfTypeAndOneInTotal<T>(this IMediator mediator) where T : IRequest
        => mediator.SentXOfTypeAndYInTotal<T>(1, 1);

    public static void PublishXOfTypeAndYInTotal<T>(this IMediator mediator, int xOfType, int yInTotal)
        where T : INotification =>
        Task.WaitAll(
            mediator.Received(xOfType).Publish(Arg.Any<INotification>()),
            mediator.ReceivedWithAnyArgs(yInTotal).Publish(Arg.Any<INotification>()));

    public static void PublishOneOfTypeAndOneInTotal<T>(this IMediator mediator) where T : INotification
        => mediator.PublishXOfTypeAndYInTotal<T>(1, 1);
}
