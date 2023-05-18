using CommNight.May._2023.CodeContainers;
using CommNight.May._2023.Domain.Data;
using CommNight.May._2023.Domain.Data.V1;
using Microsoft.Extensions.Logging;

namespace CommNight.May._2023.Domain.Services;

public class ParkingTicketService
{
    private readonly ParkingTicketRepository _parkingTicketRepository;
    private readonly RegNrValidator _regNrValidator;
    private readonly SocialSecurityNumberHelper _socialSecurityNumberHelper;
    private readonly RegNrFormatter _regNrFormatter;
    private readonly ILogger<ParkingTicketService> _logger;

    public ParkingTicketService(
        ParkingTicketRepository parkingTicketRepository, 
        RegNrValidator regNrValidator,
        SocialSecurityNumberHelper socialSecurityNumberHelper,
        RegNrFormatter regNrFormatter,
        ILogger<ParkingTicketService> logger)
    {
        _parkingTicketRepository = parkingTicketRepository;
        _regNrValidator = regNrValidator;
        _socialSecurityNumberHelper = socialSecurityNumberHelper;
        _regNrFormatter = regNrFormatter;
        _logger = logger;
    }

    public async Task IssueTicket(Guid ticketId, string socialSecurityNumber, string regNr)
    {
        if (!_socialSecurityNumberHelper.IsValid(socialSecurityNumber))
        {
            throw new ArgumentException("Invalid ssn");
        }

        if (!_regNrValidator.IsValid(regNr))
        {
            throw new ArgumentException($"The specified value {regNr} is not a valid Registration number.");
        }

        if (ticketId == Guid.Empty)
        {
            throw new ArgumentException(
                $"The specified value {ticketId} is not a valid TicketId. The value must be the non-empty GUID.");
        }

        _logger.LogInformation("Adding parking ticket");

        await _parkingTicketRepository.Add(
            new ParkingTicket(
                ticketId.ToString(), 
                _socialSecurityNumberHelper.Format(socialSecurityNumber),
                _regNrFormatter.Format(regNr)));

        _logger.LogInformation("Parking ticket added");
    }
}