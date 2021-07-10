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
        [Display(Name = "Evidence Number")]
        public string EvidenceNumber { get; set; }
        [Required]
        [Display(Name = "Institution")]
        public string Institution { get; set; }

        public Advisor Supervisor { get; set; }
        public int SupervisorID { get; set; }
        public ICollection<AdvisorContract> Advisors { get; set; }

        public Client Client { get; set; }
        public int ClientID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Enter Date")]
        public DateTime ContractEnterDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Valid Date")]
        public DateTime ContractValidSinceDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime ContractEndDate { get; set; }
    }
}
