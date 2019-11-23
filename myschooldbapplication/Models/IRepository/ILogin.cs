using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myschooldbapplication.Models.IRepository
{
  public  interface ILogin: IKidSchool<UserInfo>
    {
        int ValidateUser(UserInfo uv);
    }
}
