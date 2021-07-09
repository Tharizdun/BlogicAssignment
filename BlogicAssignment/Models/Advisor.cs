using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogicAssignment.Models
{
    public class Advisor : UserBase
    {
        [Key]
        public int AdvisorID { get; set; }
        [InverseProperty("Supervisor")]
        public ICollection<Contract> SupervisedContracts { get; set; }
        public ICollection<AdvisorContract> AdvisedContracts { get; set; }
    }
}
