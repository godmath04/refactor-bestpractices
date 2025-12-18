using System;

namespace Best_Practices.Models.Exceptions
{
    /// <summary>
    /// Exception thrown when there is a fuel-related error.
    /// </summary>
    public class FuelException : VehicleException
    {
        public FuelException() : base()
        {
        }

        public FuelException(string message) : base(message)
        {
        }

        public FuelException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
