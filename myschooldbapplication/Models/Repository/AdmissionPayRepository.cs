using Microsoft.EntityFrameworkCore;
using myschooldbapplication.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace myschooldbapplication.Models.Repository
{
    public class AdmissionPayRepository : IAdmissionPay
    {
        private readonly myschooldbContext context;
        public AdmissionPayRepository(myschooldbContext _context)
        {
            context = _context;
        }
        public ICollection<Admissiopay> GetAll()
        {
            var myschooldbContext = context.Admissiopay.Include(a => a.Ad).Include(a => a.AdT).Include(a=>a.Ad.St).Include(t=>t.Ad.Std);
            return myschooldbContext.ToList<Admissiopay>(); ;
        }

        public Admissiopay GetAllbyTermID(int id, int id1)
        {
          var res= context.Admissiopay.Find(id, id1);
            return res;
        }

        public IList<Admissiopay> GetByAdmissionId(int id)
        {
           return  context.Admissiopay.Include(k=>k.AdT).Where(k => k.AdId == id).ToList<Admissiopay>();
        }

        public ICollection<myview> GetMyviews(int typeid, int termid)
        {
            //var res= context.Myviews.Where(d => d.TypeID == typeid && d.TermID == termid);

            SqlParameter s = new SqlParameter("@type", typeid);
            SqlParameter s1 = new SqlParameter("@term", termid);
           
            var res= context.Myviews.FromSql("exec searchtermwise @type,@term",s,s1).ToList<myview>();

            return res;
        }

        public void Update(Admissiopay entity)
        {
            context.Admissiopay.Update(entity);
            context.SaveChanges();
        }

   
     
    }
}
