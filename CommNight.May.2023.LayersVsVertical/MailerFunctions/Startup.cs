using MailerFunctions;
using MailerFunctions.Helpers;
using MailerFunctions.Managers;
using MailerFunctions.Repositories;
using MailerFunctions.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace MailerFunctions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services
            .AddSingleton<IMailHelper, MailHelper>()
            .AddScoped<IUserManager, UserManager>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IMailService, MailService>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddLogging();
    }
}