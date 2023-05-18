using System.Collections.Generic;
using System.Threading.Tasks;
using MailerFunctions.Models;
using MailerFunctions.Repositories;

namespace MailerFunctions.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersWithPendingWelcome()
    {
        return await _userRepository.GetUserWithPendingWelcome();
    }

    public async Task SetWelcomeSent(User user)
    {
        await _userRepository.SetWelcomeSent(user);
    }

    public async Task RegisterNewMember(User user)
    {
        await _userRepository.AddNewMember(user);
    }
}