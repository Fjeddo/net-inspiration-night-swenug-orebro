using CommNight.May._2023.Domain.Data;
using CommNight.May._2023.Domain.Data.V1;
using CommNight.May._2023.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace CommNight.May._2023.Domain.Features.IssueParkingTicket.V1;

public record IssueParkingTicketCommand(TicketId TicketId, SocialSecurityNumber SocialSecurityNumber, RegNr RegNr) : ICommand;

public sealed class IssueParkingTicketCommandHandler : ICommandHandler<IssueParkingTicketCommand>
{
    private readonly ParkingTicketRepository _repository;

    public IssueParkingTicketCommandHandler(ParkingTicketRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(IssueParkingTicketCommand command)
    {
        await _repository.Add(
            new ParkingTicket(command.TicketId.Value.ToString(), command.SocialSecurityNumber.Value, command.RegNr.Value));
    }
}