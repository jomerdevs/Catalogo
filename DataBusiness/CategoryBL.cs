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
        
        public List<CategoryEntity> GetAll(string search)
        {
            CategoryDAL categoryData = new CategoryDAL();
            return categoryData.GetAll(search);
            
        }
        
        public List<CategoryEntity> GetCategoryById(int id)
        {
            CategoryDAL categoryData = new CategoryDAL();
            return categoryData.GetCategoryById(id);
        }

        public List<CategoryEntity> FilterCategoryList(string search)
        {
            CategoryDAL filterData = new CategoryDAL();
            return filterData.FilterCategoryList(search);
        }

        public bool CreateCategory(CategoryEntity product)
        {
            CategoryDAL createData = new CategoryDAL();
            return createData.CreateCategory(product);
        }
        
        public bool UpdateCategory(CategoryEntity product)
        {
            CategoryDAL updateData = new CategoryDAL();
            return updateData.UpdateCategory(product);
        }
        
        public string Delete(int id)
        {
            CategoryDAL deleteData = new CategoryDAL();
            return deleteData.DeleteCategory(id);
        }
    }
}
