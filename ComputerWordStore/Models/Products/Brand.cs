using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerWordStore.Models.Products
{
    [Table("products_brand")]
    public class Brand
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Required]
        [Column("name")]
        [MaxLength(150)]
        public string Name { get; set; }
        
        [Required]
        [Column("slug")]
        [MaxLength(150)]
        public string Slug { get; set; }
        public List<BrandCategory> BrandCategories { get; set; }
        public List<Product> Products { get; set; }

        public Brand()
        {
            BrandCategories = new List<BrandCategory>();
        }
    }
}