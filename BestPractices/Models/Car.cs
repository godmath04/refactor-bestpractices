using System;

namespace Best_Practices.Models
{
    /// <summary>
    /// Represents a car vehicle with four tires.
    /// </summary>
    public class Car : Vehicle
    {
        /// <summary>
        /// Gets the number of tires for a car (always 4).
        /// </summary>
        public override int Tires => 4;

        /// <summary>
        /// Initializes a new instance of the Car class.
        /// </summary>
        /// <param name="color">The color of the car.</param>
        /// <param name="brand">The brand/manufacturer of the car.</param>
        /// <param name="model">The model name of the car.</param>
        /// <param name="year">The year of the car. If null, uses current year from VehicleDefaults.</param>
        public Car(string color, string brand, string model, int? year = null)
            : base(color, brand, model, fuelLimit: 10, year: year)
        {
            // Constructor delega al constructor base
            // Parámetro year se pasa a Vehicle para aplicar defaults
        }
    }
}
