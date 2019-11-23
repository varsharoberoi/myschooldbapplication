using myschooldbapplication.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myschooldbapplication.Models.Repository
{
    public class FeeTypeRepository:KidsRepository<FeeType>,IFeeType
    {
        public FeeTypeRepository(myschooldbContext context):base(context)
        {

        }
    }
}
