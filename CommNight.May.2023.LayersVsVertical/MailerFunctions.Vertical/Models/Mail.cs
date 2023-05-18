using System;

namespace MailerFunctions.Vertical.Models;

internal record Mail(MailType Type, Member Member)
{
    public DateTimeOffset? CreatedAt { get; set; }
}

