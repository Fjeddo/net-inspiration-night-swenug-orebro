using System.Threading.Tasks;

namespace MailerFunctions.Managers;

public interface IUserManager
{
    Task SendWelcomeMail();
}
