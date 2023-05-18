using System.Runtime.CompilerServices;
using MailerFunctions.Vertical;
using MailerFunctions.Vertical.Data;
using MailerFunctions.Vertical.Infrastructure;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
[assembly: InternalsVisibleTo("MailerFunctions.Vertical.Tests")]

namespace MailerFunctions.Vertical;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder) =>
        builder.Services
            .AddScoped<IMailHelper, MailHelper>()
            .AddScoped<Db>()
            .AddMediatR(configuration => configuration
                .RegisterServicesFromAssemblyContaining<Startup>()
                .AddValidator<Features.Member.Process.CommandValidator>())
            .AddLogging();
}
