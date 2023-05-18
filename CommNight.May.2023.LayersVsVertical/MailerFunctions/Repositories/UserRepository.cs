using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailerFunctions.Models;
using Microsoft.Extensions.Logging;

namespace MailerFunctions.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<User> Storage = new()
    {
        new() {Email = "1@1.se", Name = "Ettan Ettansson"},
        new() {Email = "2@2.se", Name = "Tvåan Tvåansson"},
        new() {Email = "3@3.se", Name = "Trean Treansson"}
    };

    private readonly ILogger<UserRepository> _log;

    public UserRepository(ILogger<UserRepository> log)
    {
        _log = log;
    }

    public Task<User[]> GetUserWithPendingWelcome()
    {
        return Task.FromResult(Storage.Where(x => !x.WelcomeSent.HasValue).ToArray());
    }

    public Task SetWelcomeSent(User user)
    {
        _log.LogInformation("Setting welcome mail sent for {address}", user.Email);

        var u = Storage.FirstOrDefault(x => x.Email == user.Email);
        if (u != null)
        {
            u.WelcomeSent = DateTimeOffset.Now;

            return Task.CompletedTask;
        }

        throw new Exception($"User does not exist for {user.Email}");
    }

    public Task AddNewMember(User user)
    {
        if (Storage.Any(x => x.Email == user.Email))
        {
            throw new Exception("Member with address already exists");
        }

        Storage.Add(user);
        _log.LogInformation("Added user with {address}", user.Email);

        return Task.CompletedTask;
    }
}
