using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NT.Database.Context;

namespace NT.Database.DI;

public static class DiRegistration
{
    public static IServiceCollection RegisterEfDatabase(this IServiceCollection services, Action<DatabaseConfig> configBuilder)
    {
        ArgumentNullException.ThrowIfNull(configBuilder, nameof(configBuilder));

        DatabaseConfig dbConfig = new DatabaseConfig();
        configBuilder(dbConfig);

        ArgumentException.ThrowIfNullOrEmpty(dbConfig.ConnectionString, nameof(dbConfig.ConnectionString));

        services.AddDbContext<NutritionTrackerDbContext>(opt => opt.UseSqlServer(dbConfig.ConnectionString));

        return services;
    }
}
