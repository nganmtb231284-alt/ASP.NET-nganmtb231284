using System.ComponentModel.DataAnnotations;

namespace CafeWebsite.Models
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public ProductCategory Category { get; set; }
        
        public string Image { get; set; } = string.Empty;
        
        public decimal PriceS { get; set; }
        public decimal PriceM { get; set; }
        public decimal PriceL { get; set; }
        
        public bool Available { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
    
    public enum ProductCategory
    {
        Coffee,
        Tea,
        Smoothie,
        Juice
    }
}