namespace Best_Practices.Models
{
    /// <summary>
    /// Defines the contract for vehicle behavior including engine and fuel management.
    /// </summary>
    public interface IVehicle
    {
        /// <summary>
        /// Starts the vehicle's engine.
        /// </summary>
        void StartEngine();

        /// <summary>
        /// Stops the vehicle's engine.
        /// </summary>
        void StopEngine();

        /// <summary>
        /// Adds fuel to the vehicle's tank.
        /// </summary>
        void AddGas();

        /// <summary>
        /// Checks if the vehicle needs fuel.
        /// </summary>
        /// <returns>True if fuel is needed; otherwise, false.</returns>
        bool NeedsGas();

        /// <summary>
        /// Checks if the engine is currently running.
        /// </summary>
        /// <returns>True if the engine is on; otherwise, false.</returns>
        bool IsEngineOn();
    }
}
