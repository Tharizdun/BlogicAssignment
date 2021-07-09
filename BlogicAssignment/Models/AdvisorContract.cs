using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogicAssignment.Models
{
    public class AdvisorContract
    {
        public int AdvisorID { get; set; }
        public Advisor Advisor { get; set; }
        public int ContractID { get; set; }
        public Contract Contract { get; set; }
    }
}
