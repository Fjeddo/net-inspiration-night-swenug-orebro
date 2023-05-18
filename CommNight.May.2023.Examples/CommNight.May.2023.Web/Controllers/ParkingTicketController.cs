using CommNight.May._2023.Domain;
using CommNight.May._2023.Domain.Features.IssueParkingTicket.V1;
using CommNight.May._2023.Domain.Features.RevokeParkingTIcket;
using CommNight.May._2023.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace CommNight.May._2023.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ParkingTicketController : ControllerBase
{
    private readonly Dispatcher _dispatcher;

    public ParkingTicketController(Dispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost]
    [Route("issue")]
    public async Task<IActionResult> IssueParkingTicket(string socialSecurityNumber, string regNr)
    {
        await _dispatcher.Dispatch(new IssueParkingTicketCommand(
            TicketId.NewId(), 
            new SocialSecurityNumber(socialSecurityNumber), 
            new RegNr(regNr)));

        return Ok();
    }

    [HttpPost]
    [Route("revoke/{id}")]
    public async Task<IActionResult> RevokeParkingTicket(Guid id)
    {
        await _dispatcher.Dispatch(new RevokeTicketCommand(new TicketId(id)));
        return Ok();
    }
}