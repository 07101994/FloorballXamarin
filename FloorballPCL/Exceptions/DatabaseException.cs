using System;
using System.Collections.Generic;
using System.Text;
using FloorballPCL.Exceptions;

namespace Floorball.Exceptions
{
    class DatabaseException : FloorballException
    {

        public DatabaseException(string message, Exception innerException) : base(message, innerException) {}

        public override string ToString()
        {
            return "Server error: " + Message;
        }

    }
}
