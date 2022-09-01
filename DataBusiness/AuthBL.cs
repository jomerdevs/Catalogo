using DataAccess;
using DataEntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusiness
{
    public class AuthBL
    {
        
        public bool Login(UserEntity user)
        {
            AuthDAL loginUser = new AuthDAL();
            return loginUser.Login(user);
        }

        
        public bool Register(UserEntity user)
        {
            AuthDAL registerUser = new AuthDAL();
            return registerUser.Register(user);
        }
    }
}
