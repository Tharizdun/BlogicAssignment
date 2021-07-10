using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogicAssignment.Models
{
    public class Client : UserBase
    {
        [Key]
        public int ClientID { get; set; }
        public ICollection<Contract> Contracts { get; set; }
        [Display(Name = "Client")]
        public string FullName => FirstName + " " + LastName;
    }
}
