using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CommNight.May._2023.Domain.Features.Logging;

public sealed class LoggingCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
{
    private readonly ICommandHandler<TCommand> _decoree;
    private readonly ILogger<ICommandHandler<TCommand>> _logger;

    public LoggingCommandHandler(ICommandHandler<TCommand> decoree, ILogger<ICommandHandler<TCommand>> logger)
    {
        _decoree = decoree;
        _logger = logger;
    }

    public async Task Handle(TCommand command)
    {
        _logger.LogInformation($"Handling command {typeof(TCommand)}");

        await _decoree.Handle(command);

        _logger.LogInformation($"Handled command {typeof(TCommand)}");
    }
}

public sealed class PerformanceCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
{
    private readonly ICommandHandler<TCommand> _decoree;
    private readonly ILogger<ICommandHandler<TCommand>> _logger;

    public PerformanceCommandHandler(ICommandHandler<TCommand> decoree, ILogger<ICommandHandler<TCommand>> logger)
    {
        _decoree = decoree;
        _logger = logger;
    }

    public async Task Handle(TCommand command)
    {
        var sw = new Stopwatch();
        sw.Start();

        await _decoree.Handle(command);

        _logger.LogInformation($"Command {typeof(TCommand)} took {sw.ElapsedMilliseconds} ms");
    }
}