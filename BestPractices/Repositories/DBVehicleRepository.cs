using Best_Practices.Models;
using System;
using System.Collections.Generic;

namespace Best_Practices.Repositories
{
    /// <summary>
    /// Database implementation of the vehicle repository.
    /// This is a placeholder for future database integration.
    /// </summary>
    public class DBVehicleRepository : IVehicleRepository
    {
        /// <summary>
        /// Adds a vehicle to the database.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        /// <exception cref="NotImplementedException">This method is not yet implemented.</exception>
        public void AddVehicle(Vehicle vehicle)
        {
            throw new NotImplementedException("Database implementation is pending.");
        }

        /// <summary>
        /// Finds a vehicle by its unique identifier in the database.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle.</param>
        /// <returns>The vehicle if found.</returns>
        /// <exception cref="NotImplementedException">This method is not yet implemented.</exception>
        public Vehicle Find(Guid id)
        {
            throw new NotImplementedException("Database implementation is pending.");
        }

        /// <summary>
        /// Retrieves all vehicles from the database.
        /// </summary>
        /// <returns>A collection of all vehicles.</returns>
        /// <exception cref="NotImplementedException">This method is not yet implemented.</exception>
        public ICollection<Vehicle> GetVehicles()
        {
            throw new NotImplementedException("Database implementation is pending.");
        }
    }
}
