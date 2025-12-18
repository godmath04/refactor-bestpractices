using Best_Practices.Models;

namespace Best_Practices.ModelBuilders
{
    /// <summary>
    /// Builder class for constructing Car instances with a fluent interface.
    /// Implements the Builder pattern for flexible Car object creation.
    /// </summary>
    public class CarBuilder
    {
        private string _brand = "Ford";
        private string _model = "Mustang";
        private string _color = "Red";

        /// <summary>
        /// Sets the brand of the car.
        /// </summary>
        /// <param name="brand">The brand/manufacturer name.</param>
        /// <returns>The builder instance for method chaining.</returns>
        public CarBuilder SetBrand(string brand)
        {
            _brand = brand;
            return this;
        }

        /// <summary>
        /// Sets the model of the car.
        /// </summary>
        /// <param name="model">The model name.</param>
        /// <returns>The builder instance for method chaining.</returns>
        public CarBuilder SetModel(string model)
        {
            _model = model;
            return this;
        }

        /// <summary>
        /// Sets the color of the car.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The builder instance for method chaining.</returns>
        public CarBuilder SetColor(string color)
        {
            _color = color;
            return this;
        }

        /// <summary>
        /// Builds and returns a new Car instance with the configured properties.
        /// </summary>
        /// <returns>A new Car instance.</returns>
        public Car Build()
        {
            return new Car(_color, _brand, _model);
        }
    }
}
