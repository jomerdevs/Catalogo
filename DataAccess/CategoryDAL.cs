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
        // ================ GET ALL CATEGORIES =====================
        public List<CategoryEntity> GetAll()
        {
            List<CategoryEntity> CategoryList = null;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                try
                {
                    // Open DB Connection
                    connection.Open();

                    // Call the pocedure
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

                            while (reader.Read())
                            {
                                category = new CategoryEntity();
                                category.Id = reader.IsDBNull(propId) ? 0 : reader.GetInt32(propId);
                                category.Name = reader.IsDBNull(propName) ? "" : reader.GetString(propName);
                                category.Description = reader.IsDBNull(propDescription) ? "" : reader.GetString(propDescription);

                                CategoryList.Add(category);
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
            return CategoryList;
        }

        // ================ GET CATEGORY BY ID =====================
        public List<CategoryEntity> GetCategoryById(int id)
        {
            List<CategoryEntity> CategoryList = null;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                try
                {
                    // Open DB Connection
                    connection.Open();

                    // Call the pocedure
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

                            while (reader.Read())
                            {
                                category = new CategoryEntity();
                                category.Id = reader.IsDBNull(propId) ? 0 : reader.GetInt32(propId);
                                category.Name = reader.IsDBNull(propName) ? "" : reader.GetString(propName);
                                category.Description = reader.IsDBNull(propDescription) ? "" : reader.GetString(propDescription);

                                CategoryList.Add(category);
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
            return CategoryList;
        }

        // ================ CREATE CATEGORY ===================
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

        // ================== DELETE CATEGORY ====================
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
