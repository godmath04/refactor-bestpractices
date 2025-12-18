using Best_Practices.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Best_Practices.Infraestructure.DependencyInjection
{
    /// <summary>
    /// Extension methods for configuring application services in the DI container.
    /// </summary>
    public static class ServicesConfiguration
    {
        /// <summary>
        /// Registers application-specific services in the dependency injection container.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        /// <returns>The service collection for method chaining.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register repository with transient lifetime
            // Transient: A new instance is created each time it's requested
            services.AddTransient<IVehicleRepository, MyVehiclesRepository>();

            return services;
        }
    }
}
