﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace YF.Application;

internal static class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return services
            .AddMediatR(assembly);
    }
}