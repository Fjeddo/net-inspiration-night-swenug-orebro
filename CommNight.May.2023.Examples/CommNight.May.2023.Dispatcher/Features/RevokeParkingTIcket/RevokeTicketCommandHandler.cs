using CommNight.May._2023.Domain.ValueObjects;

namespace CommNight.May._2023.Domain.Features.RevokeParkingTIcket
{
    public record RevokeTicketCommand(TicketId TicketId) : ICommand;

    public class RevokeTicketCommandHandler : ICommandHandler<RevokeTicketCommand>
    {
        public async Task Handle(RevokeTicketCommand command)
        {
            await Task.CompletedTask;
        }
    }
}
