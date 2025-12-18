using System;

namespace Best_Practices.Models
{
    /// <summary>
    /// Represents a motorcycle vehicle with two tires.
    /// </summary>
    public class Motorcycle : Vehicle
    {
        /// <summary>
        /// Gets the number of tires for a motorcycle (always 2).
        /// </summary>
        public override int Tires => 2;

        /// <summary>
        /// Initializes a new instance of the Motorcycle class.
        /// </summary>
        /// <param name="color">The color of the motorcycle.</param>
        /// <param name="brand">The brand/manufacturer of the motorcycle.</param>
        /// <param name="model">The model name of the motorcycle.</param>
        /// <param name="year">The year of the motorcycle. If null, uses current year from VehicleDefaults.</param>
        public Motorcycle(string color, string brand, string model, int? year = null)
            : base(color, brand, model, fuelLimit: 5, year: year)
        {
            // Constructor delega al constructor base
            // Parámetro year se pasa a Vehicle para aplicar defaults
        }
    }
}
