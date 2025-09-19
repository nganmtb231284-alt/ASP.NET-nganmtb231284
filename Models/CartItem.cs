using System.ComponentModel.DataAnnotations;

namespace CafeWebsite.Models
{
    public class CartItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        public string ProductId { get; set; } = string.Empty;
        
        public Product? Product { get; set; }
        
        [Required]
        public ProductSize Size { get; set; }
        
        [Range(1, 100)]
        public int Quantity { get; set; } = 1;
        
        public List<string> Toppings { get; set; } = new();
        
        public decimal Price => Product?.GetPriceBySize(Size) ?? 0;
        public decimal Total => Price * Quantity;
    }
    
    public enum ProductSize
    {
        S,
        M,
        L
    }
    
    public static class ProductExtensions
    {
        public static decimal GetPriceBySize(this Product product, ProductSize size)
        {
            return size switch
            {
                ProductSize.S => product.PriceS,
                ProductSize.M => product.PriceM,
                ProductSize.L => product.PriceL,
                _ => product.PriceM
            };
        }
    }
}