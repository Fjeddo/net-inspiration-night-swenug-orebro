using System.Threading.Tasks;
using MailerFunctions.Models;

namespace MailerFunctions.Repositories;

public interface IUserRepository
{
    Task<User[]> GetUserWithPendingWelcome();

    Task SetWelcomeSent(User user);
    Task AddNewMember(User user);
}