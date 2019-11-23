using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myschooldbapplication.Models.IRepository
{
   public  interface IAdmissionPay
    {
        void Update(Admissiopay entity);
       ICollection<Admissiopay> GetAll();
        Admissiopay GetAllbyTermID(int id, int id1);
        IList<Admissiopay> GetByAdmissionId(int id);

        ICollection<myview> GetMyviews(int typeid, int termid);

        
      
    }
}
