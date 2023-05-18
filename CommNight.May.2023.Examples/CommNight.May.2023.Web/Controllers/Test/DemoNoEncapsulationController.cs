//using CommNight.May._2023.CodeContainers;
//using CommNight.May._2023.Domain;
//using CommNight.May._2023.Domain.Features.IssueParkingTicket.V1;
//using Microsoft.AspNetCore.Mvc;

//namespace CommNight.May._2023.Web.Controllers.Test;

//[ApiController]
//[Route("[controller]")]
//public class DemoNoEncapsulationController : ControllerBase
//{
//    private readonly Dispatcher _dispatcher;
//    private readonly SocialSecurityNumberHelper _socialSecurityNumberHelper;
//    private readonly RegNrValidator _regNrValidator;

//    public DemoNoEncapsulationController(
//        Dispatcher dispatcher,
//        SocialSecurityNumberHelper socialSecurityNumberHelper,
//        RegNrValidator regNrValidator)
//    {
//        _dispatcher = dispatcher;
//        _socialSecurityNumberHelper = socialSecurityNumberHelper;
//        _regNrValidator = regNrValidator;
//    }

//    [HttpPost]
//    public async Task<IActionResult> IssueParkingTicket(string socialSecurityNumber, string regNr)
//    {
//        if (!_socialSecurityNumberHelper.IsValid(socialSecurityNumber))
//        {
//            return BadRequest("Invalid ssn");
//        }

//        if (!_regNrValidator.IsValid(regNr))
//        {
//            return BadRequest("Invalid regnr");
//        }

//        await _dispatcher.Dispatch(new IssueParkingTicketCommand(
//            Guid.NewGuid(),
//            regNr,
//            _socialSecurityNumberHelper.Format(socialSecurityNumber)));

//        return Ok();
//    }
//}