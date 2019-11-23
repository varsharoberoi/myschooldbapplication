using System;
using System.Collections.Generic;

namespace myschooldbapplication.Models
{
    public partial class FeeType
    {
        public FeeType()
        {
            Admission1 = new HashSet<Admission1>();
        }

        public int FTId { get; set; }
        public string Feetype1 { get; set; }
        public int? FeeYear { get; set; }
        public string Feeyeartext { get; set; }
        public int? TotalFees { get; set; }

        public ICollection<Admission1> Admission1 { get; set; }
    }
}
