using CommNight.May._2023.Domain;
using CommNight.May._2023.Domain.Features.IssueParkingTicket.V2;
using CommNight.May._2023.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace CommNight.May._2023.Web.Controllers.Test
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly Dispatcher _dispatcher;

        //Only a single dependency!
        public DemoController(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> IssueParkingTicket(string socialSecurityNumber, string regNr)
        {
            await _dispatcher.Dispatch(new IssueParkingTicketCommand(
                new TicketId(Guid.NewGuid()),
                new SocialSecurityNumber(socialSecurityNumber),
                new RegNr(regNr)));

            return Ok();
        }
    }
}