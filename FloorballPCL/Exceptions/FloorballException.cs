using System;
namespace FloorballPCL.Exceptions
{
    public abstract class FloorballException : Exception
    {

        public string Title { get; set; }

        protected FloorballException(string message, Exception innerException) : base(message, innerException) 
        {
            Log();
        }

		protected FloorballException(string title, string message, Exception innerException) : base(message, innerException)
		{
            Title = title;
			Log();
		}

        void Log()
        {
			//TODO: Log (Crashlytics)
		}

    }
}
