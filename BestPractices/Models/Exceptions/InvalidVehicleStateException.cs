using System;

namespace Best_Practices.Models.Exceptions
{
    /// <summary>
    /// Exception thrown when a vehicle operation is attempted in an invalid state.
    /// </summary>
    public class InvalidVehicleStateException : VehicleException
    {
        public InvalidVehicleStateException() : base()
        {
        }

        public InvalidVehicleStateException(string message) : base(message)
        {
        }

        public InvalidVehicleStateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
