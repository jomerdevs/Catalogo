using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Catalogo.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Reference Code")]
        public string RefCode { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Model")]
        public string Model { get; set; }
        [Required]
        [DisplayName("Price")]
        public decimal Price { get; set; }
        [Required]
        [DisplayName("Brand")]
        public string Brand { get; set; }
        [Required]
        [DisplayName("CategoryId")]
        public int CategoryId { get; set; }
        [DisplayName("Category")]
        public string Category { get; set; }
    }
}