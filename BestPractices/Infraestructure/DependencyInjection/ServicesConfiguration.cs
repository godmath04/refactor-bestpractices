using Best_Practices.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Best_Practices.Infraestructure.DependencyInjection
{
    /// <summary>
    /// Métodos de extensión para configurar servicios de aplicación en el contenedor DI.
    /// </summary>
    public static class ServicesConfiguration
    {
        /// <summary>
        /// Registra los servicios específicos de la aplicación en el contenedor de inyección de dependencias.
        /// </summary>
        /// <param name="services">La colección de servicios donde agregar los servicios.</param>
        /// <param name="configuration">Configuración de la aplicación (opcional para futura configuración basada en settings).</param>
        /// <returns>La colección de servicios para encadenamiento de métodos.</returns>
        /// <remarks>
        /// CONFIGURACIÓN ACTUAL:
        /// - Usa MyVehiclesRepository (almacenamiento en memoria)
        /// - No requiere base de datos
        /// - Datos persisten solo durante la sesión de la aplicación
        ///
        /// CUANDO LA BASE DE DATOS ESTÉ LISTA:
        /// 1. Descomentar la línea de DBVehicleRepository
        /// 2. Comentar la línea de MyVehiclesRepository
        /// 3. Asegurar que DbContext esté configurado en Startup.cs
        /// </remarks>
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration = null)
        {
            // OPCIÓN 1: Repositorio en memoria (ACTUAL - EN USO)
            // Ventajas:
            // - No requiere base de datos
            // - Ideal para desarrollo y pruebas
            // - Rápido y simple
            // Desventajas:
            // - Datos se pierden al reiniciar
            // - No apto para producción
            services.AddTransient<IVehicleRepository, MyVehiclesRepository>();

            // OPCIÓN 2: Repositorio de base de datos (FUTURO)
            // Descomentar cuando el equipo de BD complete el esquema:
            // services.AddTransient<IVehicleRepository, DBVehicleRepository>();
            //
            // Asegurar también en Startup.cs:
            // services.AddDbContext<VehicleDbContext>(options =>
            //     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // OPCIÓN 3: Configuración dinámica basada en appsettings.json (AVANZADO)
            // Descomentar para permitir cambio via configuración:
            // if (configuration != null)
            // {
            //     var useDatabase = configuration.GetValue<bool>("UseDatabase", false);
            //     if (useDatabase)
            //     {
            //         services.AddTransient<IVehicleRepository, DBVehicleRepository>();
            //     }
            //     else
            //     {
            //         services.AddTransient<IVehicleRepository, MyVehiclesRepository>();
            //     }
            // }

            return services;
        }
    }
}
