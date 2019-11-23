using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myschooldbapplication.Models
{

       public class myview
    {
        public string StudentName { get; set; }
   
        public string ParentName { get; set; }

        public int? TermID { get; set; }
        public int? AdmissionID { get; set; }


        public string Term { get; set; }
        //public int TermAmount { get; set; }

        public bool? PaidStatus { get; set; }
        public int? PendingAmt { get; set; }
        public int? PaidAmount { get; set; }
        public int TermWisePending { get; set; }
        public int? TypeID { get; set; }
        public string ContactNumber { get; set; }
        public string Standard { get; set; }
        public int? TotalFees { get; set; }
       public string EmailID { get; set; }
    }
}
