using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myschooldbapplication.Models
{
    public class ReceiptStudent
    {
        [Key]
        public int? Receiptno { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? PayDate { get; set; }
        public string Pay_mode { get; set; }

        public string Chequeno { get; set; }
        public DateTime? Chequedt { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string Transactionno { get; set; }
       
    }
}
