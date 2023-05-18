using CommNight.May._2023.Domain.Features.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CommNight.May._2023.Domain
{
    public sealed class Dispatcher
    {
        private readonly IServiceProvider _provider;

        public Dispatcher(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task Dispatch<TCommand>(TCommand command) where TCommand : ICommand
        {
            var logger = _provider.GetRequiredService<ILogger<ICommandHandler<TCommand>>>();

            var handler = new LoggingCommandHandler<TCommand>(
                new PerformanceCommandHandler<TCommand>(
                _provider.GetRequiredService<ICommandHandler<TCommand>>(),logger), logger);

            await handler.Handle(command);
        }
    }
}

