using System;
namespace FloorballPCL.Exceptions
{
    public abstract class FloorballException : Exception
    {
        protected FloorballException(string message, Exception innerException) : base(message, innerException) 
        {
            Log();
        }

        void Log()
        {
			//TODO: Log (Crashlytics)
		}

    }
}
