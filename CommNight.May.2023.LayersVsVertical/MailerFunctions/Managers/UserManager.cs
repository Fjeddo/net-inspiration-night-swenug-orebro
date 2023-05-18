using System.Threading.Tasks;
using MailerFunctions.Repositories;
using MailerFunctions.Services;

namespace MailerFunctions.Managers;

public class UserManager : IUserManager
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IMailService _mailService;

    public UserManager(IUserService userService, IUserRepository userRepository,  IMailService mailService)
    {
        _userService = userService;
        _userRepository = userRepository;
        _mailService = mailService;
    }

    public async Task SendWelcomeMail()
    {
        var users = await _userRepository.GetUserWithPendingWelcome();
        foreach (var user in users)
        {
            _mailService.SendWelcomeMail(user);
            await _userService.SetWelcomeSent(user);
        }
    }
}
