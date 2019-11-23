using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace myschooldbapplication.Models
{
    public partial class Admissiopay
    {
        public int AdId { get; set; }
        public int AdTId { get; set; }
        
        [Required]
        public bool? Paystatus { get; set; }

        public Admission1 Ad { get; set; }
        public AdTypeFee AdT { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? PayDate { get; set; }
        public string Pay_mode { get; set; }

        public string Chequeno { get; set; }
        public DateTime? Chequedt { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string Transactionno { get; set; }

        public int? Receiptno { get; set; }
        public ReceiptStudent ReceiptStudent { get; set; }
       
        
    }
}
