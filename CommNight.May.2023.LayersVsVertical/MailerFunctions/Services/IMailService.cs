using MailerFunctions.Models;

namespace MailerFunctions.Services;

public interface IMailService
{
    void SendWelcomeMail(User user);
}