using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerWordStore.Models.Products
{
    [Table("products_product")]
    public class Product
    {
        [Column("id", TypeName = "uuid")]
        public Guid Id { get; set; }
        
        [Required]
        [Column("name")]
        [MaxLength(250)]
        public string Name { get; set; }
        
        [Required]
        [Column("slug")]
        [MaxLength(250)]
        public string Slug { get; set; }
        
        [Column("price", TypeName = "numeric(10,2)")]
        public decimal Price { get; set; }
        
        [Column("discount")]
        public int Discount { get; set; }
        
        [Column("number")]
        public int? Count { get; set; }
        
        [Column("short_description")]
        public string ShortDescription { get; set; }
        
        [Column("description")]
        public string Description { get; set; }
        
        [Column("characteristics", TypeName = "jsonb")]
        public string Characteristics { get; set; }
        
        [Required]
        [Column("brand_id")]
        public int BrandId { get; set; }
        
        [Required]
        [Column("category_id")]
        public int CategoryId { get; set; }
        
        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public List<ProductImages> ProductImageses { get; set; }
    }
}