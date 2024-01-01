using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence.Extensions;

public static class Registration
{
    public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ForumAppContext>(conf =>
        {
            var connectionString = configuration["ForumAppOnionDb"].ToString();
            conf.UseSqlServer(connectionString, opt =>
            {
                opt.EnableRetryOnFailure();
            });
        });
        services.AddScoped<IUserRepository,UserRepository>();
        services.AddScoped<IEntryRepository, EntryRepository>();
        services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();
        services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
        //var seeder = new Seeder();
        //seeder.SeedAsync(configuration).GetAwaiter().GetResult();
        return services;
    }
}

