using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myschooldbapplication.Models.ViewModel
{
    public class AdPay
    {
        public Admission1 Admission1 { get; set; }
        public IEnumerable<Admissiopay> ListAdmissiopay { get; set; }
    }
}
