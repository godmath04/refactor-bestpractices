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
        public Motorcycle(string color, string brand, string model)
            : base(color, brand, model, fuelLimit: 5)
        {
        }
    }
}
