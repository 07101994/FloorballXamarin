using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball.Exceptions
{
    class CommunicationException : Exception
    {
        public CommunicationException(string message, Exception innerException) : base(message,innerException) {}

        public override string ToString()
        {
            return "Kommunikációs hiba: " + Message;
        }
        
    }
}
