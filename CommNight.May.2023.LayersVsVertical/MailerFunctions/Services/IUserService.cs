using System.Collections.Generic;
using System.Threading.Tasks;
using MailerFunctions.Models;

namespace MailerFunctions.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersWithPendingWelcome();
    Task SetWelcomeSent(User user);
    Task RegisterNewMember(User user);
}
