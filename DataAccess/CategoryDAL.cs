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
    public class CategoryDAL: ConnectionDAL
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<CategoryEntity> GetAll(string search)
        {
            List<CategoryEntity> CategoryList = null;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                try
                {                    
                    connection.Open();
                    
                    using (SqlCommand cmd = new SqlCommand("CategoryList", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;                        
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            CategoryList = new List<CategoryEntity>();
                            CategoryEntity category;

                            int propId = reader.GetOrdinal("Id");
                            int propName = reader.GetOrdinal("Name");
                            int propDescription = reader.GetOrdinal("Description");
                            int propIsActive = reader.GetOrdinal("IsActive");
                            int propCreated = reader.GetOrdinal("Created");

                            while (reader.Read())
                            {
                                category = new CategoryEntity();
                                category.Id = reader.IsDBNull(propId) ? 0 : reader.GetInt32(propId);
                                category.Name = reader.IsDBNull(propName) ? "" : reader.GetString(propName);
                                category.Description = reader.IsDBNull(propDescription) ? "" : reader.GetString(propDescription);
                                category.IsActive = reader.IsDBNull(propIsActive) ? false : reader.GetBoolean(propIsActive);
                                category.Created = reader.IsDBNull(propCreated) ? DateTime.Now : reader.GetDateTime(propCreated);

                                CategoryList.Add(category);
                            }
                        }
                    }
                   
                    connection.Close();
                    
                    
                }
                catch (Exception ex)
                {
                    log.Info(ex.Message);
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }

            }

            if (!String.IsNullOrEmpty(search))
            {
                CategoryList = CategoryList.Where(s => s.Name.ToUpper().Contains(search.ToUpper())
                                || s.Description.ToUpper().Contains(search.ToUpper())).ToList();

                return CategoryList;
            }

            return CategoryList;

        }

        public List<CategoryEntity> FilterCategoryList(string search)
        {
            List<CategoryEntity> list = null;

            using (SqlConnection connectionString = new SqlConnection(Connection))
            {
                try
                {

                    connectionString.Open();

                    using (SqlCommand cmd = new SqlCommand("categoryFilter", connectionString))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@search", search);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            list = new List<CategoryEntity>();
                            CategoryEntity category;

                            int propId = reader.GetOrdinal("Id");
                            int propName = reader.GetOrdinal("Name");
                            int propDescription = reader.GetOrdinal("Description");
                            int propIsActive = reader.GetOrdinal("IsActive");
                            int propCreated = reader.GetOrdinal("Created");

                            while (reader.Read())
                            {
                                category = new CategoryEntity();
                                category.Id = reader.IsDBNull(propId) ? 0 : reader.GetInt32(propId);
                                category.Name = reader.IsDBNull(propName) ? "" : reader.GetString(propName);
                                category.Description = reader.IsDBNull(propDescription) ? "" : reader.GetString(propDescription);
                                category.IsActive = reader.IsDBNull(propIsActive) ? false : reader.GetBoolean(propIsActive);
                                category.Created = reader.IsDBNull(propCreated) ? DateTime.Now : reader.GetDateTime(propCreated);

                                list.Add(category);
                            }
                        }
                    }


                    connectionString.Close();
                }
                catch (Exception ex)
                {
                    log.Info(ex.Message);
                    throw ex;
                }
                finally
                {
                    connectionString.Close();
                }

            }

            return list;
        }

        public List<CategoryEntity> GetCategoryById(int id)
        {
            List<CategoryEntity> CategoryList = null;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                try
                {                    
                    connection.Open();
                    
                    using (SqlCommand cmd = new SqlCommand("getCategoryById", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            CategoryList = new List<CategoryEntity>();
                            CategoryEntity category;

                            int propId = reader.GetOrdinal("Id");
                            int propName = reader.GetOrdinal("Name");
                            int propDescription = reader.GetOrdinal("Description");
                            int propIsActive = reader.GetOrdinal("IsActive");
                            int propCreated = reader.GetOrdinal("Created");

                            while (reader.Read())
                            {
                                category = new CategoryEntity();
                                category.Id = reader.IsDBNull(propId) ? 0 : reader.GetInt32(propId);
                                category.Name = reader.IsDBNull(propName) ? "" : reader.GetString(propName);
                                category.Description = reader.IsDBNull(propDescription) ? "" : reader.GetString(propDescription);
                                category.IsActive = reader.IsDBNull(propIsActive) ? false : reader.GetBoolean(propIsActive);
                                category.Created = reader.IsDBNull(propCreated) ? DateTime.Now : reader.GetDateTime(propCreated);

                                CategoryList.Add(category);
                            }
                        }
                    }
                    
                    connection.Close();
                }
                catch (Exception ex)
                {
                    log.Info(ex.Message);
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }

            }
            return CategoryList;
        }
        
        public bool CreateCategory(CategoryEntity category)
        {
            int id = 0;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                try
                {

                    using (SqlCommand cmd = new SqlCommand("createCategory", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@categoryName", category.Name);
                        cmd.Parameters.AddWithValue("@description", category.Description);
                        cmd.Parameters.AddWithValue("@isActive", category.IsActive);
                        
                        connection.Open();
                        id = cmd.ExecuteNonQuery(); 
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
                    log.Info(ex.Message);
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }

            }

        }

        // =================== UPDATE CATEGORY =====================
        public bool UpdateCategory(CategoryEntity category)
        {
            int i = 0;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                try
                {

                    using (SqlCommand cmd = new SqlCommand("updateCategory", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", category.Id);
                        cmd.Parameters.AddWithValue("@categoryName", category.Name);
                        cmd.Parameters.AddWithValue("@description", category.Description);
                        cmd.Parameters.AddWithValue("@isActive", category.IsActive);
                        
                        connection.Open();
                        i = cmd.ExecuteNonQuery(); 
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
                    log.Info(ex.Message);
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }

            }

        }
        
        public string DeleteCategory(int id)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                SqlCommand cmd = new SqlCommand("deleteCategory", connection);
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
