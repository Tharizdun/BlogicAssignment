using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogicAssignment.Models
{
    public class Contract
    {
        [Key]
        public int ContractID { get; set; }
        [Required]
        public string EvidenceNumber { get; set; }
        [Required]
        public string Institution { get; set; }

        public Advisor Supervisor { get; set; }
        public int SupervisorID { get; set; }
        public ICollection<AdvisorContract> Advisors { get; set; }

        public Client Client { get; set; }
        public int ClientID { get; set; }

        [Required]
        public DateTime ContractEnterDate { get; set; }
        [Required]
        public DateTime ContractValidSinceDate { get; set; }
        [Required]
        public DateTime ContractEndDate { get; set; }
    }
}
