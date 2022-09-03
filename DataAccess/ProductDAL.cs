using DataEntityLayer;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAL: ConnectionDAL
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public List<ProductEntity> GetAll(string search)
        {
            List<ProductEntity> ProductList = null;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                try
                {
                    
                    connection.Open();
                    
                    using (SqlCommand cmd = new SqlCommand("ProductList", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            ProductList = new List<ProductEntity>();
                            ProductEntity product;

                            int propId = reader.GetOrdinal("Id");
                            int propRefCode = reader.GetOrdinal("RefCode");
                            int propName = reader.GetOrdinal("Name");
                            int propModel = reader.GetOrdinal("Model");
                            int propPrice = reader.GetOrdinal("Price");
                            int propBrand = reader.GetOrdinal("Brand");
                            int propCategoryId = reader.GetOrdinal("CategoryId");
                            int propCategory = reader.GetOrdinal("Category");

                            while (reader.Read())
                            {
                                product = new ProductEntity();
                                product.Id = reader.IsDBNull(propId) ? 0 : reader.GetInt32(propId);
                                product.RefCode = reader.IsDBNull(propRefCode) ? "" : reader.GetString(propRefCode);
                                product.Name = reader.IsDBNull(propName) ? "" : reader.GetString(propName);
                                product.Model = reader.IsDBNull(propModel) ? "" : reader.GetString(propModel);
                                product.Price = reader.IsDBNull(propPrice) ? 0 : reader.GetDecimal(propPrice);
                                product.Brand = reader.IsDBNull(propBrand) ? "" : reader.GetString(propBrand);
                                product.CategoryId = reader.IsDBNull(propCategoryId) ? 0 : reader.GetInt32(propCategoryId);
                                product.Category = reader.IsDBNull(propCategory) ? "" : reader.GetString(propCategory);

                                ProductList.Add(product);
                            }
                        }
                    }                    
                    
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }

            if (!String.IsNullOrEmpty(search))
            {
                ProductList = ProductList.Where(s => s.Name.ToUpper().Contains(search.ToUpper())
                                || s.Model.ToUpper().Contains(search.ToUpper()) || s.Brand.ToUpper().Contains(search.ToUpper()) || s.Category.ToUpper().Contains(search.ToUpper())).ToList();

                return ProductList;
            }

            return ProductList;
        }

        public List<ProductEntity> FilterProductsList(string search)
        {
            List<ProductEntity> list = null;

            using (SqlConnection connectionString = new SqlConnection(Connection))
            {
                try
                {
                    
                    connectionString.Open();
                    
                    using (SqlCommand cmd = new SqlCommand("productListSearch", connectionString))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@search", search);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            list = new List<ProductEntity>();
                            ProductEntity product;

                            int propId = reader.GetOrdinal("Id");
                            int propRefCode = reader.GetOrdinal("RefCode");
                            int propName = reader.GetOrdinal("Name");
                            int propModel = reader.GetOrdinal("Model");
                            int propPrice = reader.GetOrdinal("Price");
                            int propBrand = reader.GetOrdinal("Brand");
                            int propCategory = reader.GetOrdinal("Category");

                            while (reader.Read())
                            {
                                product = new ProductEntity();
                                product.Id = reader.IsDBNull(propId) ? 0 : reader.GetInt32(propId);
                                product.RefCode = reader.IsDBNull(propRefCode) ? "" : reader.GetString(propRefCode);
                                product.Name = reader.IsDBNull(propName) ? "" : reader.GetString(propName);
                                product.Model = reader.IsDBNull(propModel) ? "" : reader.GetString(propModel);
                                product.Price = reader.IsDBNull(propPrice) ? 0 : reader.GetDecimal(propPrice);
                                product.Brand = reader.IsDBNull(propBrand) ? "" : reader.GetString(propBrand);
                                product.Category = reader.IsDBNull(propCategory) ? "" : reader.GetString(propCategory);

                                list.Add(product);
                            }
                        }
                    }                   
                    
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    throw ex;
                }
                finally
                {
                    connectionString.Close();
                }

            }

            return list;
        }
        
        public List<ProductEntity> GetProductById(int id)
        {
            List<ProductEntity> ProductList = null;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                try
                {
                    
                    connection.Open();
                    
                    using (SqlCommand cmd = new SqlCommand("getProductById", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader != null)
                        {
                            ProductList = new List<ProductEntity>();
                            ProductEntity product;

                            int propId = reader.GetOrdinal("Id");
                            int propRefCode = reader.GetOrdinal("RefCode");
                            int propName = reader.GetOrdinal("Name");
                            int propModel = reader.GetOrdinal("Model");
                            int propPrice = reader.GetOrdinal("Price");
                            int propBrand = reader.GetOrdinal("Brand");
                            int propCategoryId = reader.GetOrdinal("CategoryId");
                            int propCategory = reader.GetOrdinal("Category");

                            while (reader.Read())
                            {
                                product = new ProductEntity();
                                product.Id = reader.IsDBNull(propId) ? 0 : reader.GetInt32(propId);
                                product.RefCode = reader.IsDBNull(propRefCode) ? "" : reader.GetString(propRefCode);
                                product.Name = reader.IsDBNull(propName) ? "" : reader.GetString(propName);
                                product.Model = reader.IsDBNull(propModel) ? "" : reader.GetString(propModel);
                                product.Price = reader.IsDBNull(propPrice) ? 0 : reader.GetDecimal(propPrice);
                                product.Brand = reader.IsDBNull(propBrand) ? "" : reader.GetString(propBrand);
                                product.CategoryId = reader.IsDBNull(propCategoryId) ? 0 : reader.GetInt32(propCategoryId);
                                product.Category = reader.IsDBNull(propCategory) ? "" : reader.GetString(propCategory);

                                ProductList.Add(product);
                            }
                        }
                    }                   
                    
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
            return ProductList;
        }
        
        public bool CreateProduct(ProductEntity product)
        {
            int id = 0;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                try
                {

                    using (SqlCommand cmd = new SqlCommand("createProduct", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@refCode", product.RefCode);
                        cmd.Parameters.AddWithValue("@productName", product.Name);
                        cmd.Parameters.AddWithValue("@model", product.Model);
                        cmd.Parameters.AddWithValue("@price", product.Price);
                        cmd.Parameters.AddWithValue("@brand", product.Brand);
                        cmd.Parameters.AddWithValue("@categoryId", product.CategoryId);
                        
                        connection.Open();
                        id = cmd.ExecuteNonQuery(); 
                        
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
                    logger.Error(ex.Message);
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }

            }

        }
        
        public bool UpdateProduct(ProductEntity product)
        {
            int i = 0;

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                try
                {

                    using (SqlCommand cmd = new SqlCommand("updateProduct", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", product.Id);
                        cmd.Parameters.AddWithValue("@refCode", product.RefCode);
                        cmd.Parameters.AddWithValue("@productName", product.Name);
                        cmd.Parameters.AddWithValue("@model", product.Model);
                        cmd.Parameters.AddWithValue("@price", product.Price);
                        cmd.Parameters.AddWithValue("@brand", product.Brand);
                        cmd.Parameters.AddWithValue("@categoryId", product.CategoryId);
                        
                        connection.Open();
                        i = cmd.ExecuteNonQuery(); 
                        
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
                    logger.Error(ex.Message);
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }

            }

        }
        
        public string DeleteProduct(int id)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                SqlCommand cmd = new SqlCommand("deleteProduct", connection);
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
