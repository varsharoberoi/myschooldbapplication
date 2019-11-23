using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myschooldbapplication.Models.ViewModel
{
    public class FeeTermModel
    {
        public FeeType FeeTypes { get; set; }
        public int Noofterms { get; set; }
        
        public string AdTypeFees { get; set; }
        public List<AdTypeFee> adtypes { get; set; }

    }
    public class tests
    {
        public Dictionary<string,string> adtypes { get; set; }
    }
}
