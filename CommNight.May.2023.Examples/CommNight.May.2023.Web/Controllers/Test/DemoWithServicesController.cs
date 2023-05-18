using CommNight.May._2023.CodeContainers;
using CommNight.May._2023.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommNight.May._2023.Web.Controllers.Test;

[ApiController]
[Route("[controller]")]
public class DemoWithServicesController : ControllerBase
{
    private readonly ParkingTicketService _service;
    private readonly SocialSecurityNumberHelper _socialSecurityNumberHelper;
    private readonly RegNrValidator _regNrValidator;

    public DemoWithServicesController(
        ParkingTicketService service,
        SocialSecurityNumberHelper socialSecurityNumberHelper,
        RegNrValidator regNrValidator)
    {
        _service = service;
        _socialSecurityNumberHelper = socialSecurityNumberHelper;
        _regNrValidator = regNrValidator;
    }

    [HttpPost]
    public async Task<IActionResult> IssueParkingTicket(string socialSecurityNumber, string regNr)
    {
        if (!_socialSecurityNumberHelper.IsValid(socialSecurityNumber))
        {
            return BadRequest("Invalid ssn");
        }

        if (!_regNrValidator.IsValid(regNr))
        {
            return BadRequest("Invalid regnr");
        }

        await _service.IssueTicket(new Guid(), socialSecurityNumber, regNr);

        return Ok();
    }
}