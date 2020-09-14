using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerWordStore.Models.Products
{
    [Table("products_image")]
    public class ProductImages
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Required]
        [Column("slug")]
        [MaxLength(250)]
        public string Slug { get; set; }
        
        [Required]
        [Column("image")]
        [MaxLength(100)]
        public string Image { get; set; }
        
        [Required]
        [Column("product_id")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}