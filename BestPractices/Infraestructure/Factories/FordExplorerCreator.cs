using Best_Practices.ModelBuilders;
using Best_Practices.Models;

namespace Best_Practices.Infraestructure.Factories
{
    /// <summary>
    /// Factory for creating Ford Explorer vehicle instances.
    /// Creates a black Ford Explorer using the CarBuilder with custom configuration.
    /// </summary>
    public class FordExplorerCreator : Creator
    {
        /// <summary>
        /// Creates a new Ford Explorer with black color.
        /// </summary>
        /// <returns>A configured Ford Explorer instance.</returns>
        public override Vehicle Create()
        {
            var builder = new CarBuilder();
            return builder
                .SetModel("Explorer")
                .SetColor("Black")
                .Build();
        }
    }
}
