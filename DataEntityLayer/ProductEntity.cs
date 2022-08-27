using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DataEntityLayer
{
    public class ProductEntity
    {
       
        public int Id { get; set; }
        
        [DisplayName("Reference Code")]
        public string RefCode { get; set; }
        
        [DisplayName("Product Name")]
        public string Name { get; set; }
        
        [DisplayName("Model")]
        public string Model { get; set; }
        
        [DisplayName("Price")]
        public decimal Price { get; set; }
        
        [DisplayName("Brand")]
        public string Brand { get; set; }
        
        [DisplayName("CategoryId")]
        public int CategoryId { get; set; }
        [DisplayName("Category")]
        public string Category { get; set; }
    }
}
