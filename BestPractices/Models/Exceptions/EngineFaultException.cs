using System;

namespace Best_Practices.Models.Exceptions
{
    /// <summary>
    /// Exception thrown when there is an engine-related fault.
    /// </summary>
    public class EngineFaultException : VehicleException
    {
        public EngineFaultException() : base()
        {
        }

        public EngineFaultException(string message) : base(message)
        {
        }

        public EngineFaultException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
