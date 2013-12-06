using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseLibrary
{
    public class Author
    {
        public int author_id { get; set; }
        public string author_name { get; set; }
        public string author_surname { get; set; }
        public string author_middle_name { get; set; }
        public DateTime author_birth_date { get; set; }
    }
}
