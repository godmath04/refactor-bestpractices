using Best_Practices.Models;
using System;
using System.Collections.Generic;

namespace Best_Practices.Repositories
{
    /// <summary>
    /// Defines the contract for vehicle repository operations.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Retrieves all vehicles from the repository.
        /// </summary>
        /// <returns>A collection of all vehicles.</returns>
        ICollection<Vehicle> GetVehicles();

        /// <summary>
        /// Adds a new vehicle to the repository.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        void AddVehicle(Vehicle vehicle);

        /// <summary>
        /// Finds a vehicle by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle.</param>
        /// <returns>The vehicle if found; otherwise, null.</returns>
        Vehicle Find(Guid id);
    }
}
