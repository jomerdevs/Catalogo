using DataEntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AuthDAL: ConnectionDAL
    {
        public static string HashSha256(string password)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(password));

                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();

        }

        // ================ LOGIN ===============
        public string Login(UserEntity user)
        {
            int id;

            if (user.Password != null)
            {
                user.Password = HashSha256(user.Password);

                using (SqlConnection connection = new SqlConnection(Connection))
                {
                     using (SqlCommand cmd = new SqlCommand("validateUser", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@email", user.Email);
                            cmd.Parameters.AddWithValue("@password", user.Password);

                            // Open DB Connection
                            connection.Open();

                            user.Id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                            SqlDataReader reader = cmd.ExecuteReader();

                            if (reader != null)
                            {

                                int propIsAdmin = reader.GetOrdinal("IsAdmin");

                                while (reader.Read())
                                {
                                    user.IsAdmin = reader.IsDBNull(propIsAdmin) ? false : reader.GetBoolean(propIsAdmin);
                                }
                            }

                            connection.Close();

                        }

                        id = user.Id;

                        // CHECK IF USER EXISTS
                        if (id != 0)
                        {
                            return "found";
                        }
                        else
                        {
                            return "notFound";
                        }
                    }                    

                
            }
            else
            {
                return "noPassword";
            }
           
        }

        // ====================== REGISTER =======================
        public bool Register(UserEntity user)
        {
            bool registered;
            string message;
            
            user.Password = HashSha256(user.Password);

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("Register", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@lastName", user.LastName);
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@phone", user.Phone);
                        cmd.Parameters.AddWithValue("@password", user.Password);
                        cmd.Parameters.Add("@registered", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@message", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                        // Open DB Connection
                        connection.Open();
                        cmd.ExecuteNonQuery(); // Returns 1 for success and 0 when fail

                        registered = Convert.ToBoolean(cmd.Parameters["@registered"].Value);
                        message = cmd.Parameters["@message"].Value.ToString();
                        connection.Close();
                    }

                    if (registered)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception)
                {
                    connection.Close();
                    return false;
                }

            }             

        }
    
    }
        
}
