using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerWordStore.Models.Products
{
    [Table("products_brand_categories")]
    public class BrandCategory
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("brand_id")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        
        [Column("category_id")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}