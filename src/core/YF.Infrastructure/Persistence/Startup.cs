using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YF.Application.Common.Persistance;
using YF.Domain.Common.Contracts;
using YF.Infrastructure.Persistence.Context;
using YF.Infrastructure.Persistence.Repository;

namespace YF.Infrastructure.Persistence;

internal static class Startup
{
    internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddDbContext<ApplicationDbContext>(opt =>
                opt.UseInMemoryDatabase("YFdb"))
            .AddScoped<ApplicationDbContextInitializator>()
            .AddRepositories();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(ApplicationDbRepository<>));

        foreach (var aggregateRootType in 
            typeof(IAggregateRoot).Assembly.GetExportedTypes()
                .Where(t => typeof(IAggregateRoot).IsAssignableFrom(t) && t.IsClass)
                .ToList())
        {
            services.AddScoped(typeof(IReadRepository<>).MakeGenericType(aggregateRootType), sp =>
                sp.GetRequiredService(typeof(IRepository<>).MakeGenericType(aggregateRootType)));
        }

        return services;
    }
}
