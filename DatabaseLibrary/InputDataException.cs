using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseLibrary
{
    public class InputDataException : Exception
    {
        public InputDataException(string message) : base(message) { }      
    }
}
