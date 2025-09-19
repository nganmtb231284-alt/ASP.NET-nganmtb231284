using System.ComponentModel.DataAnnotations;

namespace CafeWebsite.Models
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string? UserId { get; set; }
        public User? User { get; set; }
        
        [Required]
        public string CustomerName { get; set; } = string.Empty;
        
        [Required]
        [Phone]
        public string CustomerPhone { get; set; } = string.Empty;
        
        [EmailAddress]
        public string? CustomerEmail { get; set; }
        
        [Required]
        public string CustomerAddress { get; set; } = string.Empty;
        
        public string? Notes { get; set; }
        
        public List<OrderItem> Items { get; set; } = new();
        
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; } = 0;
        public decimal Total { get; set; }
        
        public string? PromotionCode { get; set; }
        public string? PromotionId { get; set; }
        public Promotion? Promotion { get; set; }
        
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        
        public PaymentMethod PaymentMethod { get; set; }
        
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
    
    public class OrderItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string OrderId { get; set; } = string.Empty;
        
        public string ProductId { get; set; } = string.Empty;
        
        public Product? Product { get; set; }
        
        public string ProductName { get; set; } = string.Empty;
        
        public ProductSize Size { get; set; }
        
        public int Quantity { get; set; }
        
        public decimal Price { get; set; }
        
        public decimal Total => Price * Quantity;
        
        public List<string> Toppings { get; set; } = new();
    }
    
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Preparing,
        Delivering,
        Completed,
        Cancelled
    }
    
    public enum PaymentMethod
    {
        MoMo,
        ZaloPay,
        BankTransfer,
        COD
    }
    
    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed
    }
}