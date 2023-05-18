using System.Net.Mail;

namespace MailerFunctions.Helpers;

public interface IMailHelper
{
    void SendMailMessage(MailMessage mailMessage);
}