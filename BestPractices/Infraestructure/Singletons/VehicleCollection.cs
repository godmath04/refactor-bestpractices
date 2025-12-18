using Best_Practices.Models;
using System;
using System.Collections.Generic;

namespace Best_Practices.Infraestructure.Singletons
{
    /// <summary>
    /// Thread-safe singleton collection for storing vehicles in memory.
    /// Implements the Singleton pattern using Lazy&lt;T&gt; for thread-safety.
    /// </summary>
    public class VehicleCollection
    {
        private static readonly Lazy<VehicleCollection> _lazyInstance =
            new Lazy<VehicleCollection>(() => new VehicleCollection());

        /// <summary>
        /// Gets the singleton instance of VehicleCollection.
        /// Thread-safe implementation using Lazy&lt;T&gt;.
        /// </summary>
        public static VehicleCollection Instance => _lazyInstance.Value;

        /// <summary>
        /// Gets the collection of vehicles.
        /// </summary>
        public ICollection<Vehicle> Vehicles { get; }

        /// <summary>
        /// Private constructor to prevent external instantiation.
        /// Initializes the vehicle collection.
        /// </summary>
        private VehicleCollection()
        {
            Vehicles = new List<Vehicle>();
        }
    }
}
