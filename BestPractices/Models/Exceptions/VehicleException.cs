using System;

namespace Best_Practices.Models.Exceptions
{
    /// <summary>
    /// Base exception for all vehicle-related errors.
    /// </summary>
    public class VehicleException : Exception
    {
        public VehicleException() : base()
        {
        }

        public VehicleException(string message) : base(message)
        {
        }

        public VehicleException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
