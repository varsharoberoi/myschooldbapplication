using Microsoft.EntityFrameworkCore;
using myschooldbapplication.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myschooldbapplication.Models.Repository
{
    public class AdmissionRepository:KidsRepository<Admission1>,IAdmission
    {
      
        public AdmissionRepository(myschooldbContext context):base(context)
        {
           
        }
        public  override IEnumerable<Admission1> GetAll()
        {
            
            var myschooldbContext = context.Admission1.Include(a => a.FT).Include(a => a.St).Include(a => a.Std);
            return myschooldbContext.ToList();
        }
        public override Admission1 GetById(int id)
        {
            var myschooldbContext = context.Admission1.Include(a => a.FT).Include(a => a.St).Include(a => a.Std).Include(d=>d.Admissiopay);
         
            var res = myschooldbContext.Where(d => d.AdId == id).First();
            return res;
        }

    }
}
