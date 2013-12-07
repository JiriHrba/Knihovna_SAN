using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseLibrary
{
    /// <summary>
    /// K vyhozeni teto vyjimky dojde v pripade, ze se klient chce zaregistrovat na akci, chce si pujcit ci rezervovat knihu, ale platnost jeho 
    /// clenstvi jiz skoncila.
    /// </summary>
    public class MembershipExpiredException : Exception
    {
        public MembershipExpiredException(string message) : base(message) { }                 
    }
}
