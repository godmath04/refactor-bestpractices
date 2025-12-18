using Best_Practices.Infraestructure.Singletons;
using Best_Practices.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Best_Practices.Repositories
{
    /// <summary>
    /// In-memory implementation of the vehicle repository using a singleton collection.
    /// </summary>
    public class MyVehiclesRepository : IVehicleRepository
    {
        private readonly VehicleCollection _memoryCollection;

        /// <summary>
        /// Initializes a new instance of the MyVehiclesRepository class.
        /// </summary>
        public MyVehiclesRepository()
        {
            _memoryCollection = VehicleCollection.Instance;
        }

        /// <summary>
        /// Adds a vehicle to the in-memory collection.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when vehicle is null.</exception>
        public void AddVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle), "Vehicle cannot be null.");

            _memoryCollection.Vehicles.Add(vehicle);
        }

        /// <summary>
        /// Finds a vehicle by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle.</param>
        /// <returns>The vehicle if found; otherwise, null.</returns>
        public Vehicle Find(Guid id)
        {
            return _memoryCollection.Vehicles.FirstOrDefault(v => v.Id.Equals(id));
        }

        /// <summary>
        /// Retrieves all vehicles from the in-memory collection.
        /// </summary>
        /// <returns>A collection of all vehicles.</returns>
        public ICollection<Vehicle> GetVehicles()
        {
            return _memoryCollection.Vehicles;
        }
    }
}
