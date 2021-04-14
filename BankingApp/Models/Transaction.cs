using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.BussinessLogic
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [Required]
        public Account account { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        
        public double? Credit { get; set; }
        public double? Debit { get; set; }
        [Required]
        public double Balance { get; set; }
        [Required]
        [StringLength(15)]
        public string Reference { get; set; }       
    }
}
