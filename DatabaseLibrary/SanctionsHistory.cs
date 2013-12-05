using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseLibrary
{
    public class SanctionsHistory
    {
        public int sanction_id { get; set; }
        public DateTime sanction_grant { get; set; }
        public DateTime sanction_paid { get; set; }
        public string sanction_desc { get; set; }
        public int client_id { get; set; }
        public int stype_id { get; set; }
    }
}
