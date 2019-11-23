using myschooldbapplication.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myschooldbapplication.Models.Repository
{
    public class LoginRepository : KidsRepository<UserInfo>, ILogin
    {
        public LoginRepository(myschooldbContext context) : base(context)
        {

        }
        public int ValidateUser(UserInfo uv)
        {
            var res = context.UserInfos.Where(d => d.UserID.Equals(uv.UserID)).First();
            if (res == null)
            {
                return 1;
            }
            else if (res.Password == uv.Password)
            {
                return 0;
            }
            else
            {
                return 2;
            }

        }
    }
}
