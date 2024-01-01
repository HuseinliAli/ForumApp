using Bogus;
using Common.Infrastructure;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace Persistence.Context;

public class Seeder
{
    private static List<User> GetUsers()
    {
        var result = new Faker<User>("az");
        result.RuleFor(x => x.Id, x => Guid.NewGuid());
        result.RuleFor(x => x.CreatedAt, x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now));
        result.RuleFor(x => x.FirstName, x => x.Person.FirstName);
        result.RuleFor(x => x.LastName, x => x.Person.LastName);
        result.RuleFor(x => x.EmailAddress, x => x.Internet.Email());
        result.RuleFor(x => x.EmailConfirmed, x => x.PickRandom(true, false));
        result.RuleFor(x => x.UserName, x => x.Internet.UserName());
        result.RuleFor(x => x.Password, x => PasswordEncryptor.Encrypt(x.Internet.Password()));
        return result.Generate(500);
    }

    public async Task SeedAsync(IConfiguration configuration)
    {
        var dbBuilder = new DbContextOptionsBuilder();
        dbBuilder.UseSqlServer(configuration["ForumAppOnionDb"]);
        var context = new ForumAppContext(dbBuilder.Options);
        var users = GetUsers();
        var userIds = users.Select(x => x.Id);
        await context.Users.AddRangeAsync(users);

        var guids = Enumerable.Range(0, 100).Select(x => Guid.NewGuid()).ToList();
        int counter = 0;
        var entries = new Faker<Entry>("az")
            .RuleFor(x => x.Id, x => guids[counter++])
            .RuleFor(x => x.CreatedAt, x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
            .RuleFor(x => x.Subject, x => x.Lorem.Sentence(5, 5))
            .RuleFor(x => x.Content, x => x.Lorem.Paragraph(2))
            .RuleFor(x => x.UserId, x => x.PickRandom(userIds))
            .Generate(100);
        await context.Entries.AddRangeAsync(entries);

        var comments = new Faker<EntryComment>("az")
            .RuleFor(x => x.Id, x => Guid.NewGuid())
            .RuleFor(x => x.CreatedAt, x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
            .RuleFor(x => x.Content, x => x.Lorem.Paragraph(2))
            .RuleFor(x => x.UserId, x => x.PickRandom(userIds))
            .RuleFor(x => x.EntryId, x => x.PickRandom(guids))
            .Generate(2000);
        await context.EntryComments.AddRangeAsync(comments);
        await context.SaveChangesAsync();
    }
}