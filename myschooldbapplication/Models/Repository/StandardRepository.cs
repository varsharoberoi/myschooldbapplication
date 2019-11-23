using myschooldbapplication.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myschooldbapplication.Models.Repository
{
    public class StandardRepository:KidsRepository<Std>,IStandard
    {
        public StandardRepository(myschooldbContext context):base(context)
        {

        }
       
    }
}
