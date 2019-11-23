using Microsoft.EntityFrameworkCore;
using myschooldbapplication.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myschooldbapplication.Models.Repository
{
    public class AdmissionTypeRepository:KidsRepository<AdTypeFee>,IAdmissionType
    {
        public AdmissionTypeRepository(myschooldbContext context) : base(context)
        {

        }

        public void GetRecord(int ad_id, int f_t_id)
        {
            throw new NotImplementedException();
        }

        public ICollection<AdTypeFee> GetRecordById(int? id)
        {
            //var res= context.AdTypeFee.Where(d => d.AdTId == id);
            // res.Include(d => d.Admissiopay);
            // return res;
            if (id.HasValue)
            {
                var res = context.AdTypeFee.Include(d => d.feetype).Where(d => d.FTId == id);
                return res.ToList<AdTypeFee>();
            }
            else
            {
                var res = context.AdTypeFee.Include(d => d.feetype);
                   return res.ToList<AdTypeFee>();
            }
            
        }

        void IAdmissionType.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
