using System.Threading.Tasks;
using MailerFunctions.Managers;
using MailerFunctions.Models;
using MailerFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace MailerFunctions;

public class FunctionTriggers
{
    private readonly IUserManager _userManager;
    private readonly IUserService _userService;

    public FunctionTriggers(IUserManager userManager, IUserService userService)
    {
        _userManager = userManager;
        _userService = userService;
    }

    [FunctionName(nameof(ManualSendWelcomeMail))]
    public async Task<IActionResult> ManualSendWelcomeMail([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        await ExecuteSendWelcomeMail();
        return new OkResult();
    }

    [FunctionName(nameof(SendWelcomeMail))]
    public async Task SendWelcomeMail([TimerTrigger(SendWelcomeMailNCrontab, RunOnStartup = false)] TimerInfo timerInfo)
    {
        await ExecuteSendWelcomeMail();
    }

    private async Task ExecuteSendWelcomeMail() => await _userManager.SendWelcomeMail();

    [FunctionName(nameof(ManualRegisterNewMember))]
    public async Task<IActionResult> ManualRegisterNewMember([HttpTrigger(AuthorizationLevel.Function, "post")] RegistrationRequest req)
    {
        await ExecuteRegisterNewMember(req);
        return new OkResult();
    }

    //[FunctionName(nameof(RegisterNewMember))]
    //public async Task RegisterNewMember([ServiceBusTrigger("member-registrations")] RegistrationRequest message)
    //{
    //    await ExecuteRegisterNewMember(message);
    //}

    private async Task ExecuteRegisterNewMember(RegistrationRequest request) => await _userService.RegisterNewMember(new User {Email = request.Email, Name = request.Name});

#if DEBUG
    private const string SendWelcomeMailNCrontab = Constants.NeverRunExpression;
#else
    private const string SendWelcomeMailNCrontab = Constants.OnceEveryHourAtTheHour;
#endif
}

public class Constants
{
    public const string NeverRunExpression = "0 0 0 31 2 0";
    public const string OnceEveryHourAtTheHour = "0 0 * * * *";
}
