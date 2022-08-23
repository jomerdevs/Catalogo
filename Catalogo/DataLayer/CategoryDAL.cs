using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Catalogo.Models;

namespace Catalogo.DataLayer
{
    public class CategoryDAL
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["connection"].ToString();

        // ================ GET ALL CATEGORIES ================
        public List<Category> GetAll()
        {
            List<Category> CategoryList = null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
                            CategoryList = new List<Category>();
                            Category category;

                            int propId = reader.GetOrdinal("Id");
                            int propName = reader.GetOrdinal("Name");
                            int propDescription = reader.GetOrdinal("Description");

                            while (reader.Read())
                            {
                                category = new Category();
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

        // ============  CREATE CATEGORY  =================
        public bool CreateCategory(Category category)
        {
            int id = 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
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

        // ========== GET CATEGORY BY ID =============
        public List<Category> GetCategoryById(int id)
        {
            List<Category> CategoryList = null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
                            CategoryList = new List<Category>();
                            Category category;

                            int propId = reader.GetOrdinal("Id");
                            int propName = reader.GetOrdinal("Name");
                            int propDescription = reader.GetOrdinal("Description");

                            while (reader.Read())
                            {
                                category = new Category();
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

        // =============== UPDATE CATEGORY ===============
        public bool UpdateCategory(Category category)
        {
            int i = 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
    }
}