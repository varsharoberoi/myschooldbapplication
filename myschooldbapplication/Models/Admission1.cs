using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace myschooldbapplication.Models
{
    public partial class Admission1
    {
        public Admission1()
        {
            Admissiopay = new HashSet<Admissiopay>();
        }
        [Required]
        
        public int AdId { get; set; }
        public int? StId { get; set; }
        public int? StdId { get; set; }
        public int? FTId { get; set; }
        public int? FeeYear { get; set; }
        public int? PaidAmount { get; set; }
        public int? Totalfees { get; set;}
        public int? Pendingamt { get; set; }

        public FeeType FT { get; set; }
        public Student St { get; set; }
        public Std Std { get; set; }
        public ICollection<Admissiopay> Admissiopay { get; set; }
    }
}
