using Microsoft.Extensions.DependencyInjection;
using NT.Ef.Repositories.Abstractions;
using NT.Ef.Repositories.Implementations.Repositories;

namespace NT.Ef.Repositories.DI;

public static class DiRegistration
{
    public static IServiceCollection RegisterRepository(this IServiceCollection services)
    {
        services.AddScoped<IFoodNutritionRepository, FoodNutritionRepository>();
        services.AddScoped<IFoodLogRepository, FoodLogRepository>();
        services.AddScoped<IFoodItemRepository, FoodItemRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
