using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myschooldbapplication.Models.ViewModel
{
    public class ListModel
    {
        public int ctrl1 { get; set; }
        public int AdId { get; set; }
        public List<EnrollModel> EnrollModels { get; set; }
        public UserSearch UserSearch { get; set; }
        public EnrollModel EnrollModel { get; set; }
    }
}
