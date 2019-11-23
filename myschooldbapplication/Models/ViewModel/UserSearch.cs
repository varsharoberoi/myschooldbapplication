using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myschooldbapplication.Models.ViewModel
{
    public class UserSearch
    {
        [Display(Name="Admission ID")]
        public int Ad_ID { get; set; }
        [Display(Name ="Student Name")]
        public string St_Name { get; set; }
        [Display(Name ="Standard")]
        public int? Standard { get; set; }
        
    }
}
