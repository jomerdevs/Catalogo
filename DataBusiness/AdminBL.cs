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
        
        public List<UserEntity> GetAll(string search)
        {
            AdminDAL usersData = new AdminDAL();
            return usersData.GetAll(search);

        }

        public string PasswordToCompare()
        {
            UserEntity userAux = new UserEntity();
            return userAux.Password;
        }        

        
        public List<UserAuxEntity> GetUserById(int id)
        {
            AdminDAL userData = new AdminDAL();
            return userData.GetUserById(id);
        }

        public List<UserEntity> FilterUserList(string search)
        {
            AdminDAL filterData = new AdminDAL();
            return filterData.FilterUserList(search);
        }

        public bool CreateUser(UserAuxEntity user)
        {
            AdminDAL createData = new AdminDAL();
            return createData.CreateUser(user);
        }
        
        public bool UpdateUser(UserAuxEntity user)
        {
            AdminDAL updateData = new AdminDAL();
            return updateData.UpdateUser(user);
        }
        
        public string Delete(int id)
        {
            AdminDAL deleteData = new AdminDAL();
            return deleteData.DeleteUser(id);
        }
    }
}
