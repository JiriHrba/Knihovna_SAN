using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class ForgotPasswordException : Exception
    {
        public ForgotPasswordException(string message) : base(message) { }
    }
}
