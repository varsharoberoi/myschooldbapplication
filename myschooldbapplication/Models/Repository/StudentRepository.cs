using myschooldbapplication.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myschooldbapplication.Models.Repository
{
    public class StudentRepository :KidsRepository<Student>,IStudent
    {
   
        public StudentRepository(myschooldbContext _context):base(_context)
        {
   
        }

    }
}
