using System.Collections.Generic;
using MailerFunctions.Vertical.Models;

namespace MailerFunctions.Vertical.Data;

internal class Db
{
    private static readonly List<Member> _members = new()
    {
        new(Address: "1@1.se", Name: "Ettan Ettansson"),
        new(Address: "2@2.se", Name: "Tvåan Tvåansson"),
        new(Address: "3@3.se", Name: "Trean Treansson")
    };

    private static readonly List<Mail> _mail = new()
    {
        new Mail(MailType.Welcome, _members[0]),
        new Mail(MailType.Welcome, _members[1]),
        new Mail(MailType.Welcome, _members[2])
    };

    public List<Member> Members => _members;
    public List<Mail> Mail => _mail;
}
