using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntityLayer
{
    public class CategoryEntity
    {
        
        public int Id { get; set; }
        
        [DisplayName("Category Name")]
        public string Name { get; set; }
        
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}
