using DataAccess;
using DataEntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusiness
{
    public class AdminBL
    {
        // GET ALL USERS
        public List<UserEntity> GetAll()
        {
            AdminDAL usersData = new AdminDAL();
            return usersData.GetAll();

        }

        // GET USER BY ID
        public List<UserAuxEntity> GetUserById(int id)
        {
            AdminDAL userData = new AdminDAL();
            return userData.GetUserById(id);
        }

        // CREATE USER
        public bool CreateUser(UserAuxEntity user)
        {
            AdminDAL createData = new AdminDAL();
            return createData.CreateUser(user);
        }

        // UPDATE USER
        public bool UpdateUser(UserAuxEntity user)
        {
            AdminDAL updateData = new AdminDAL();
            return updateData.UpdateUser(user);
        }

        // DELETE USER
        public string Delete(int id)
        {
            AdminDAL deleteData = new AdminDAL();
            return deleteData.DeleteUser(id);
        }
    }
}
