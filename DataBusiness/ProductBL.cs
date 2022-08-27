using DataAccess;
using DataEntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusiness
{
    public class ProductBL
    {
        // GET ALL PRODUCTS
        public List<ProductEntity> GetAll()
        {
            ProductDAL ProductData = new ProductDAL();
            return ProductData.GetAll();
        }

        // GET PRODUCT BY ID
        public List<ProductEntity> GetProductById(int id)
        {
            ProductDAL ProductData = new ProductDAL();
            return ProductData.GetProductById(id);
        }

        // CREATE PRODUCT
        public bool CreateProduct(ProductEntity product)
        {
            ProductDAL createData = new ProductDAL();
            return createData.CreateProduct(product);
        }

        // UPDATE PRODUCT
        public bool UpdateProduct(ProductEntity product)
        {
            ProductDAL updateData = new ProductDAL();
            return updateData.UpdateProduct(product);
        }

        // DELETE PRODUCT
        public string Delete(int id)
        {
            ProductDAL deleteData = new ProductDAL();
            return deleteData.DeleteProduct(id);
        }

    }
}
