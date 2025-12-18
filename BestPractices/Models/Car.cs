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
        public Car(string color, string brand, string model)
            : base(color, brand, model, fuelLimit: 10)
        {
        }
    }
}
