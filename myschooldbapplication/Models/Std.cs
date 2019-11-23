using System;
using System.Collections.Generic;

namespace myschooldbapplication.Models
{
    public partial class Std
    {
        public Std()
        {
            Admission1 = new HashSet<Admission1>();
        }

        public int StdId { get; set; }
        public string Stname { get; set; }

        public ICollection<Admission1> Admission1 { get; set; }
    }
}
