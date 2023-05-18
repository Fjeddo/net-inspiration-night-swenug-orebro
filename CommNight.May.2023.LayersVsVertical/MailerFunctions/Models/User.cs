using System;

namespace MailerFunctions.Models;

public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTimeOffset? WelcomeSent { get; set; }
}