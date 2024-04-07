using NT.Database.DI;

namespace DailyNutritionCaloriesTracker.Server.Configuration.Extensions;

public static class DependencyRegistration
{
    public static IServiceCollection RegisterDependencyInjection(this IServiceCollection services, ConfigurationManager configurationManager) 
    {
        services.RegisterEfDatabase(option =>
        {
            option.ConnectionString = configurationManager.GetConnectionString("NutritionTracker");
        });

        return services;
    }
}
