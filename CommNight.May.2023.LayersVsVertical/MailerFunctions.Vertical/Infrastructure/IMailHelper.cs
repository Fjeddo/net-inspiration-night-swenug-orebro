using System.Net.Mail;

namespace MailerFunctions.Vertical.Infrastructure;

public interface IMailHelper
{
    void SendMailMessage(MailMessage mailMessage);
}