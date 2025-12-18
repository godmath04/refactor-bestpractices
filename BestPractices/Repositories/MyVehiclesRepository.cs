using Best_Practices.Infraestructure.Singletons;
using Best_Practices.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Best_Practices.Repositories
{
    /// <summary>
    /// Implementación en memoria del repositorio de vehículos usando una colección singleton.
    /// </summary>
    /// <remarks>
    /// ESTADO: EN USO - IMPLEMENTACIÓN ACTIVA
    ///
    /// Esta implementación permite:
    /// - Pruebas sin base de datos
    /// - Desarrollo rápido
    /// - Demostración de funcionalidad
    ///
    /// Limitaciones:
    /// - Los datos se pierden al reiniciar la aplicación
    /// - No apto para producción
    /// - Sin persistencia entre sesiones
    ///
    /// Ventajas del patrón Repository:
    /// - El controlador no sabe que usa memoria en lugar de BD
    /// - Fácil cambio a DBVehicleRepository cuando esté listo
    /// - Mismo contrato (IVehicleRepository) para ambas implementaciones
    /// </remarks>
    public class MyVehiclesRepository : IVehicleRepository
    {
        private readonly VehicleCollection _memoryCollection;

        /// <summary>
        /// Inicializa una nueva instancia de la clase MyVehiclesRepository.
        /// </summary>
        /// <remarks>
        /// Obtiene la instancia singleton de VehicleCollection.
        /// El singleton es thread-safe gracias al uso de Lazy&lt;T&gt;.
        /// </remarks>
        public MyVehiclesRepository()
        {
            _memoryCollection = VehicleCollection.Instance;
        }

        /// <summary>
        /// Agrega un vehículo a la colección en memoria.
        /// </summary>
        /// <param name="vehicle">El vehículo a agregar.</param>
        /// <exception cref="ArgumentNullException">Se lanza cuando el vehículo es nulo.</exception>
        /// <remarks>
        /// El vehículo se agrega a la colección singleton compartida.
        /// Los cambios son inmediatamente visibles para todos los usuarios de la colección.
        /// </remarks>
        public void AddVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle), "El vehículo no puede ser nulo.");

            _memoryCollection.Vehicles.Add(vehicle);
        }

        /// <summary>
        /// Busca un vehículo por su identificador único.
        /// </summary>
        /// <param name="id">El identificador único del vehículo.</param>
        /// <returns>El vehículo si se encuentra; de lo contrario, null.</returns>
        /// <remarks>
        /// Usa LINQ para buscar en la colección en memoria.
        /// El rendimiento es O(n) donde n es el número de vehículos.
        /// Para producción con BD, el rendimiento sería O(1) con índice en ID.
        /// </remarks>
        public Vehicle Find(Guid id)
        {
            return _memoryCollection.Vehicles.FirstOrDefault(v => v.Id.Equals(id));
        }

        /// <summary>
        /// Obtiene todos los vehículos de la colección en memoria.
        /// </summary>
        /// <returns>Una colección de todos los vehículos.</returns>
        /// <remarks>
        /// Retorna la colección completa sin filtros ni paginación.
        /// Para producción con muchos registros, considerar implementar paginación.
        /// </remarks>
        public ICollection<Vehicle> GetVehicles()
        {
            return _memoryCollection.Vehicles;
        }
    }
}
