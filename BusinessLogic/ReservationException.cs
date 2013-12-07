using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class ReservationException : Exception
    {
        public ReservationException(string message) : base(message) { }
    }
}
