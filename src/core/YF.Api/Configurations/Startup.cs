using YF.Api.Configurations.ConfigsDto;
using YF.Infrastructure.Common.ConfigsDto;

namespace YF.Api.Configurations;

internal static class Startup
{
    internal static ConfigureHostBuilder AddConfiguration(this ConfigureHostBuilder host)
    {
        host.ConfigureAppConfiguration((config) =>
        {
            const string configurationDir = "Configurations";

            config.AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"{configurationDir}/currencies.json", false, true)
                .AddEnvironmentVariables();
        });

        return host;
    }

    internal static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<AbstractApiSettings>(config.GetSection(nameof(AbstractApiSettings)));
        services.Configure<CoinMarketCapApiSettings>(config.GetSection(nameof(CoinMarketCapApiSettings)));

        return services;
    }
}
