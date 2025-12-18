using Best_Practices.Infraestructure.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Best_Practices
{
    /// <summary>
    /// Configura los servicios y el pipeline de solicitudes de la aplicación.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase Startup.
        /// </summary>
        /// <param name="configuration">La configuración de la aplicación.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Obtiene la configuración de la aplicación.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configura los servicios para la aplicación.
        /// Este método es llamado por el runtime.
        /// </summary>
        /// <param name="services">La colección de servicios a configurar.</param>
        /// <remarks>
        /// CONFIGURACIÓN ACTUAL:
        /// - MVC con vistas Razor
        /// - Repositorio en memoria (sin base de datos)
        ///
        /// CUANDO LA BASE DE DATOS ESTÉ LISTA, AGREGAR:
        /// services.AddDbContext<VehicleDbContext>(options =>
        ///     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        /// </remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            // Agregar soporte para MVC con vistas
            services.AddControllersWithViews();

            // Registrar servicios de aplicación usando método de extensión
            // Actualmente usa almacenamiento en memoria
            // Pasar Configuration permite configuración dinámica futura
            services.AddApplicationServices(Configuration);

            // TODO: Cuando la base de datos esté lista, descomentar:
            // services.AddDbContext<VehicleDbContext>(options =>
            //     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        /// <summary>
        /// Configura el pipeline de solicitudes HTTP.
        /// Este método es llamado por el runtime.
        /// </summary>
        /// <param name="app">El constructor de la aplicación.</param>
        /// <param name="env">El entorno de hospedaje web.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configurar páginas de error según el entorno
            if (env.IsDevelopment())
            {
                // En desarrollo, mostrar página de excepciones detallada
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // En producción, usar página de error personalizada
                app.UseExceptionHandler("/Home/Error");
                // Habilitar HTTP Strict Transport Security (HSTS)
                // Valor por defecto: 30 días
                // Para producción, considerar aumentar este valor
                app.UseHsts();
            }

            // Redirigir HTTP a HTTPS automáticamente
            app.UseHttpsRedirection();

            // Habilitar archivos estáticos (CSS, JavaScript, imágenes)
            app.UseStaticFiles();

            // Habilitar enrutamiento
            app.UseRouting();

            // Habilitar autorización
            // Actualmente no se usa, pero está disponible para futuras mejoras
            app.UseAuthorization();

            // Configurar endpoints de la aplicación
            app.UseEndpoints(endpoints =>
            {
                // Ruta por defecto: Home/Index
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
