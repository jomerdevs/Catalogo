using DataEntityLayer;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusiness
{
    public class CategoryBL
    {
        // GET ALL CATEGORIES
        public List<CategoryEntity> GetAll()
        {
            CategoryDAL productsData = new CategoryDAL();
            return productsData.GetAll();
            
        }

        // GET CATEGORY BY ID
        public List<CategoryEntity> GetCategoryById(int id)
        {
            CategoryDAL productData = new CategoryDAL();
            return productData.GetCategoryById(id);
        }

        // CREATE CATEGORY
        public bool CreateCategory(CategoryEntity product)
        {
            CategoryDAL createData = new CategoryDAL();
            return createData.CreateCategory(product);
        }

        // UPDATE CATEGORY
        public bool UpdateCategory(CategoryEntity product)
        {
            CategoryDAL updateData = new CategoryDAL();
            return updateData.UpdateCategory(product);
        }

        // DELETE CATEGORY
        public string Delete(int id)
        {
            CategoryDAL deleteData = new CategoryDAL();
            return deleteData.DeleteCategory(id);
        }
    }
}
