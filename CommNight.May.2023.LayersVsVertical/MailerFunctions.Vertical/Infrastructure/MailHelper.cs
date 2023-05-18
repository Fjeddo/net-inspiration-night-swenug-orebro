using System.Net.Mail;
using Microsoft.Extensions.Logging;

namespace MailerFunctions.Vertical.Infrastructure;

public class MailHelper : IMailHelper
{
    private readonly SmtpClient _smtpClient;
    private readonly ILogger<MailHelper> _log;

    public MailHelper(SmtpClient smtpClient, ILogger<MailHelper> log)
    {
        _smtpClient = smtpClient;
        _log = log;
    }

    public void SendMailMessage(MailMessage mailMessage)
    {
        //_smtpClient.SendAsync(mailMessage, null);
        _log.LogInformation("Sending mail to {address}", mailMessage.To);
    }
}