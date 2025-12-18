using Best_Practices.ModelBuilders;
using Best_Practices.Models;

namespace Best_Practices.Infraestructure.Factories
{
    /// <summary>
    /// Factory for creating Ford Mustang vehicle instances.
    /// Creates a red Ford Mustang using the default CarBuilder configuration.
    /// </summary>
    public class FordMustangCreator : Creator
    {
        /// <summary>
        /// Creates a new Ford Mustang with default configuration (Red color).
        /// </summary>
        /// <returns>A configured Ford Mustang instance.</returns>
        public override Vehicle Create()
        {
            var builder = new CarBuilder();
            return builder.Build();
        }
    }
}
