using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball.Exceptions
{
    class FloorballException : Exception
    {

        public FloorballException(string message, Exception innerException) : base(message, innerException) {}

        public override string ToString()
        {
            return "Szerver hiba: " + Message;
        }

    }
}
