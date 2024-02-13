using Infrastructure.Database.Models;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Repositories;

namespace CarServiceApi.Extensions;

public static class SeviceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IGenericRepository<Car>, GenericRepository<Car>>();
        services.AddScoped<IGenericRepository<Service>, GenericRepository<Service>>();
        services.AddScoped<IGenericRepository<CarAssignedToService>, GenericRepository<CarAssignedToService>>();
        return services;
    }
}
