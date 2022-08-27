using DataEntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AdminDAL: ConnectionDAL
    {
        // ================ GET ALL USERS ==================
        public List<UserEntity> GetAll()
        {
            List<UserEntity> UsersList = null;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                try
                {
                    // Open DB Connection
                    connection.Open();

                    // Call the pocedure
                    using (SqlCommand cmd = new SqlCommand("UsersList", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            UsersList = new List<UserEntity>();
                            UserEntity user;

                            int propId = reader.GetOrdinal("Id");
                            int propFirstName = reader.GetOrdinal("FirstName");
                            int propLastName = reader.GetOrdinal("LastName");
                            int propEmail = reader.GetOrdinal("Email");
                            int propPhone = reader.GetOrdinal("Phone");
                            int propIsAdmin = reader.GetOrdinal("IsAdmin");


                            while (reader.Read())
                            {
                                user = new UserEntity();
                                user.Id = reader.IsDBNull(propId) ? 0 : reader.GetInt32(propId);
                                user.FirstName = reader.IsDBNull(propFirstName) ? "" : reader.GetString(propFirstName);
                                user.LastName = reader.IsDBNull(propLastName) ? "" : reader.GetString(propLastName);
                                user.Email = reader.IsDBNull(propEmail) ? "" : reader.GetString(propEmail);
                                user.Phone = reader.IsDBNull(propPhone) ? "" : reader.GetString(propPhone);
                                user.IsAdmin = reader.IsDBNull(propIsAdmin) ? false : reader.GetBoolean(propIsAdmin);


                                UsersList.Add(user);
                            }
                        }
                    }

                    // Close after we get the data
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                }
            }

            return UsersList;
        }

        // ================ GET USER BY ID ==================
        public List<UserAuxEntity> GetUserById(int id)
        {
            List<UserAuxEntity> UsersList = null;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                try
                {
                    // Open DB Connection
                    connection.Open();

                    // Call the pocedure
                    using (SqlCommand cmd = new SqlCommand("getUserById", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            UsersList = new List<UserAuxEntity>();
                            UserAuxEntity user;

                            int propId = reader.GetOrdinal("Id");
                            int propFirstName = reader.GetOrdinal("FirstName");
                            int propLastName = reader.GetOrdinal("LastName");
                            int propEmail = reader.GetOrdinal("Email");
                            int propPhone = reader.GetOrdinal("Phone");
                            int propPassword = reader.GetOrdinal("Password");
                            int propIsAdmin = reader.GetOrdinal("IsAdmin");

                            while (reader.Read())
                            {
                                user = new UserAuxEntity();
                                user.Id = reader.IsDBNull(propId) ? 0 : reader.GetInt32(propId);
                                user.FirstName = reader.IsDBNull(propFirstName) ? "" : reader.GetString(propFirstName);
                                user.LastName = reader.IsDBNull(propLastName) ? "" : reader.GetString(propLastName);
                                user.Email = reader.IsDBNull(propEmail) ? "" : reader.GetString(propEmail);
                                user.Phone = reader.IsDBNull(propPhone) ? "" : reader.GetString(propPhone);
                                user.Password = reader.IsDBNull(propPassword) ? "" : reader.GetString(propPassword);
                                user.IsAdmin = reader.IsDBNull(propIsAdmin) ? false : reader.GetBoolean(propIsAdmin);


                                UsersList.Add(user);
                            }
                        }
                    }

                    // Close after we get the data
                    connection.Close();
                }
                catch (Exception ex)
                {
                    connection.Close();
                }
            }
            return UsersList;
        }

        // ================ CREATE USER ====================
        public bool CreateUser(UserAuxEntity user)
        {
            int id = 0;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                try
                {

                    using (SqlCommand cmd = new SqlCommand("createUser", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@lastName", user.LastName);
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@phone", user.Phone);
                        cmd.Parameters.AddWithValue("@password", user.Password);
                        cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);

                        // Open DB Connection
                        connection.Open();
                        id = cmd.ExecuteNonQuery(); // Returns 1 for success and 0 when fail
                        connection.Close();
                    }
                    if (id > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    connection.Close();
                    return false;
                }

            }

        }

        // ================ UPDATE USER ====================
        public bool UpdateUser(UserAuxEntity user)
        {
            int i = 0;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                try
                {

                    using (SqlCommand cmd = new SqlCommand("updateUser", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", user.Id);
                        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@lastName", user.LastName);
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@phone", user.Phone);
                        cmd.Parameters.AddWithValue("@password", user.Password);
                        cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);

                        // Open DB Connection
                        connection.Open();
                        i = cmd.ExecuteNonQuery(); // Returns 1 for success and 0 when fail
                        connection.Close();
                    }
                    if (i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    connection.Close();
                    return false;
                }

            }

        }

        // ================ DELETE USER ====================
        public string DeleteUser(int id)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                SqlCommand cmd = new SqlCommand("deleteUser", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.Add("@RETURNMESSAGE", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                cmd.ExecuteNonQuery();
                result = cmd.Parameters["@RETURNMESSAGE"].Value.ToString();
                connection.Close();
            }

            return result;
        }


    }
}
