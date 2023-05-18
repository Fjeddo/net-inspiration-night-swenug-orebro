using System.Net.Mail;
using MailerFunctions.Helpers;
using MailerFunctions.Models;

namespace MailerFunctions.Services;

public class MailService : IMailService
{
    private readonly IMailHelper _mailHelper;

    public MailService(IMailHelper mailHelper)
    {
        _mailHelper = mailHelper;
    }

    public void SendWelcomeMail(User user)
    {
        var mailMessage = CreateWelcomeMail(user.Email, user.Name);
        _mailHelper.SendMailMessage(mailMessage);
    }

    private static MailMessage CreateWelcomeMail(string to, string name)
        => new(
            "noreply@tomtens-hus.fi",
            to,
            "Välkommen!", $"Vi är glada att just du, {name}, har registrerat dig hos oss!");
}
