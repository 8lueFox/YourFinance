using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using YF.Infrastructure.Common;
using YF.Infrastructure.Persistence;

namespace YF.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddServices()
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddPersistence(config);
    }
}
