using Microsoft.Extensions.DependencyInjection;
using TypeType.Lib.Data;

namespace TypeType.Lib;

public static class ServiceExtensions
{
    public static IServiceCollection AddTypeTypeDb(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton(sp => new DbContext(connectionString));
        return services;
    }
}
