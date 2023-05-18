using CommNight.May._2023.Domain.ValueObjects;

namespace CommNight.May._2023.Domain.Data.V2;

public record ParkingTicket(TicketId TicketId, SocialSecurityNumber SocialSecurityNumber, RegNr RegNr);
