using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NT.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT.Database.DI;

public static class DiRegistration
{
    public static IServiceCollection RegisterEfDatabase(this IServiceCollection services, Action<DatabaseConfig> configBuilder)
    {
        ArgumentNullException.ThrowIfNull(configBuilder, nameof(configBuilder));

        var dbConfig = new DatabaseConfig();
        configBuilder(dbConfig);

        ArgumentException.ThrowIfNullOrEmpty(dbConfig.ConnectionString, nameof(dbConfig.ConnectionString));

        services.AddDbContext<NutritionTrackerDbContext>(opt => opt.UseSqlServer(dbConfig.ConnectionString));

        return services;
    }
}
