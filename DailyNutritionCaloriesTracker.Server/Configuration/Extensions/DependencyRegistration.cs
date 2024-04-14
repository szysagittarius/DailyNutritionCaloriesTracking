using AutoMapper;
using NT.Application.DI;
using NT.Database.DI;
using NT.Ef.Repositories.DI;
using NutritionTracker.Api.Profilers;

namespace DailyNutritionCaloriesTracker.Server.Configuration.Extensions;

public static class DependencyRegistration
{
    public static IServiceCollection RegisterDependencyInjection(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        //profilers
        MapperConfiguration dtoMapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new FoodNutritionDtoProfiler());
            cfg.AddProfile(new FoodItemDtoProfiler());
            cfg.AddProfile(new FoodLogDtoProfiler());
            cfg.AddProfile(new UserDtoProfiler());
        });

        IMapper dtoMapper = dtoMapperConfig.CreateMapper();
        services.AddSingleton(dtoMapper);



        services.RegisterApplicationServices();

        services.RegisterEfDatabase(option =>
        {
            option.ConnectionString = configurationManager.GetConnectionString("NutritionTracker");
        });

        services.RegisterRepository();

        return services;
    }
}
