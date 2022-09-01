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
        public List<ProductEntity> GetAll(string search)
        {
            ProductDAL ProductData = new ProductDAL();
            return ProductData.GetAll(search);
        }
        
        public List<ProductEntity> GetProductById(int id)
        {
            ProductDAL ProductData = new ProductDAL();
            return ProductData.GetProductById(id);
        }
        
        public List<ProductEntity> FilterProductList(string search)
        {
            ProductDAL filterData = new ProductDAL();
            return filterData.FilterProductsList(search);
        }
        
        public bool CreateProduct(ProductEntity product)
        {
            ProductDAL createData = new ProductDAL();
            return createData.CreateProduct(product);
        }
        
        public bool UpdateProduct(ProductEntity product)
        {
            ProductDAL updateData = new ProductDAL();
            return updateData.UpdateProduct(product);
        }
        
        public string Delete(int id)
        {
            ProductDAL deleteData = new ProductDAL();
            return deleteData.DeleteProduct(id);
        }

    }
}
