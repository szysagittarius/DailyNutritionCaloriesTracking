using Microsoft.Extensions.DependencyInjection;

namespace NT.Application.DI;
public static class DiRegistration
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        //services.AddScoped<IUnitOfWork, UnitOfWork>();
        //services.AddScoped<ICustomerService, CustomerService>();
        //services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
