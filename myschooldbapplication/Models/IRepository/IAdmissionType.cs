using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myschooldbapplication.Models.IRepository
{
public    interface IAdmissionType : IKidSchool<AdTypeFee>
    {
        void GetAll();
        void GetRecord(int ad_id, int f_t_id);
        ICollection<AdTypeFee> GetRecordById(int? id);
    }
}
