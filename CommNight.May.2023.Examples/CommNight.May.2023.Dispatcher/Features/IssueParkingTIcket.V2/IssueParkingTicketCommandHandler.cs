using CommNight.May._2023.Domain.Data;
using CommNight.May._2023.Domain.Data.V2;
using CommNight.May._2023.Domain.ValueObjects;

namespace CommNight.May._2023.Domain.Features.IssueParkingTicket.V2;

public record IssueParkingTicketCommand(TicketId TicketId, SocialSecurityNumber SocialSecurityNumber, RegNr RegNr) : ICommand;

public sealed class IssueParkingTicketCommandHandler : ICommandHandler<IssueParkingTicketCommand>
{
    private readonly ParkingTicketRepository _repository;

    public IssueParkingTicketCommandHandler(
        ParkingTicketRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(IssueParkingTicketCommand command)
    {
        //Zero null checks
        //Zero input validation
        //zero formatting

        await _repository.Add(
            new ParkingTicket(command.TicketId, command.SocialSecurityNumber, command.RegNr));
    }
}