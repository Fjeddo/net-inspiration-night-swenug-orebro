using MediatR;
using Microsoft.Extensions.Logging;

namespace MailerFunctions.Vertical.Infrastructure;

public static class LoggerExtensions
{
    public static void LogRequestHandling<TCommand>(this ILogger<IRequestHandler<TCommand>> log)
        where TCommand : IRequest
    {
        var tCommand = typeof(TCommand);

        log.LogInformation("Handling {command}", $"{tCommand.DeclaringType}.{tCommand.Name}");
    }
}
