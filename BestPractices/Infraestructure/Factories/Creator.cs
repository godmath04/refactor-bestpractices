using Best_Practices.Models;

namespace Best_Practices.Infraestructure.Factories
{
    /// <summary>
    /// Abstract factory base class for creating vehicle instances.
    /// Implements the Factory Method pattern.
    /// </summary>
    public abstract class Creator
    {
        /// <summary>
        /// Creates a new vehicle instance.
        /// </summary>
        /// <returns>A configured vehicle instance.</returns>
        public abstract Vehicle Create();
    }
}
