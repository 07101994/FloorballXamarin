using System;
namespace FloorballPCL.Exceptions
{
    public class ValidationException : FloorballException
    {
		public ValidationException(string message, Exception innerException) : base(message,innerException) { }

        public ValidationException(string title, string message, Exception innerException) : base(title, message, innerException) { }

		public override string ToString()
		{
            return Title + ": " + Message;
		}
    
    }
}
