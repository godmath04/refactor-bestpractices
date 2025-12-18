using System;
using Best_Practices.Models.Exceptions;

namespace Best_Practices.Models
{
    /// <summary>
    /// Abstract base class representing a vehicle with engine and fuel management capabilities.
    /// </summary>
    public abstract class Vehicle : IVehicle
    {
        #region Constants
        private const double GasIncrementPerPump = 0.1; // Gallons per pump action
        private const double MinimumGasToStart = 0.01; // Minimum fuel required to start engine
        #endregion

        #region Private Fields
        private bool _isEngineOn;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the unique identifier for this vehicle.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the number of tires for this vehicle type.
        /// </summary>
        public abstract int Tires { get; }

        /// <summary>
        /// Gets the color of the vehicle.
        /// </summary>
        public string Color { get; }

        /// <summary>
        /// Gets the brand/manufacturer of the vehicle.
        /// </summary>
        public string Brand { get; }

        /// <summary>
        /// Gets the model name of the vehicle.
        /// </summary>
        public string Model { get; }

        /// <summary>
        /// Gets the current fuel level in gallons.
        /// </summary>
        public double Gas { get; private set; }

        /// <summary>
        /// Gets the maximum fuel capacity in gallons.
        /// </summary>
        public double FuelLimit { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Vehicle class.
        /// </summary>
        /// <param name="color">The color of the vehicle.</param>
        /// <param name="brand">The brand/manufacturer of the vehicle.</param>
        /// <param name="model">The model name of the vehicle.</param>
        /// <param name="fuelLimit">The maximum fuel capacity in gallons. Default is 10.</param>
        /// <exception cref="ArgumentNullException">Thrown when brand or model is null or whitespace.</exception>
        /// <exception cref="ArgumentException">Thrown when fuelLimit is not positive.</exception>
        protected Vehicle(string color, string brand, string model, double fuelLimit = 10)
        {
            // Guard clauses for validation
            if (string.IsNullOrWhiteSpace(brand))
                throw new ArgumentNullException(nameof(brand), "Brand cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentNullException(nameof(model), "Model cannot be null or empty.");

            if (fuelLimit <= 0)
                throw new ArgumentException("Fuel limit must be a positive value.", nameof(fuelLimit));

            Id = Guid.NewGuid();
            Color = string.IsNullOrWhiteSpace(color) ? "White" : color; // Default color
            Brand = brand;
            Model = model;
            FuelLimit = fuelLimit;
            Gas = 0; // Start with empty tank
            _isEngineOn = false;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds fuel to the vehicle's tank.
        /// </summary>
        /// <exception cref="FuelException">Thrown when the fuel tank is already full.</exception>
        public void AddGas()
        {
            if (Gas >= FuelLimit)
            {
                throw new FuelException("Fuel tank is already full.");
            }

            Gas += GasIncrementPerPump;

            // Ensure we don't exceed fuel limit due to floating point precision
            if (Gas > FuelLimit)
            {
                Gas = FuelLimit;
            }
        }

        /// <summary>
        /// Starts the vehicle's engine.
        /// </summary>
        /// <exception cref="EngineFaultException">Thrown when the engine is already running.</exception>
        /// <exception cref="FuelException">Thrown when there is insufficient fuel to start the engine.</exception>
        public void StartEngine()
        {
            if (_isEngineOn)
            {
                throw new EngineFaultException("Engine is already running.");
            }

            if (NeedsGas())
            {
                throw new FuelException("Insufficient fuel to start engine. Please refuel.");
            }

            _isEngineOn = true;
        }

        /// <summary>
        /// Checks if the vehicle needs fuel.
        /// </summary>
        /// <returns>True if fuel is needed; otherwise, false.</returns>
        public bool NeedsGas()
        {
            return Gas < MinimumGasToStart;
        }

        /// <summary>
        /// Checks if the engine is currently running.
        /// </summary>
        /// <returns>True if the engine is on; otherwise, false.</returns>
        public bool IsEngineOn()
        {
            return _isEngineOn;
        }

        /// <summary>
        /// Stops the vehicle's engine.
        /// </summary>
        /// <exception cref="EngineFaultException">Thrown when the engine is already stopped.</exception>
        public void StopEngine()
        {
            if (!_isEngineOn)
            {
                throw new EngineFaultException("Engine is already stopped.");
            }

            _isEngineOn = false;
        }
        #endregion
    }
}
