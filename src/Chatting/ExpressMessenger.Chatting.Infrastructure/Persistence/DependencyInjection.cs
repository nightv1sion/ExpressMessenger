using ExpressMessenger.Chatting.Application;
using ExpressMessenger.Chatting.Domain.ChatAggregate;
using ExpressMessenger.Chatting.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpressMessenger.Chatting.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection RegisterPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((_, options) =>
            options
                .UseNpgsql(configuration.GetConnectionString("Database"))
                .UseSnakeCaseNamingConvention());
        return services
            .AddScoped<IChatRepository, ChatRepository>()
            .AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
    }
}