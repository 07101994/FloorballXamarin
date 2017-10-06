using System;
using System.Collections.Generic;
using System.Text;
using FloorballPCL.Exceptions;

namespace Floorball.Exceptions
{
    public class CommunicationException : FloorballException
    {

        public CommunicationException(string message, Exception innerException) : base(message,innerException) {}

        public override string ToString()
        {
            return "Network Error: " + Message;
        }
        
    }
}
