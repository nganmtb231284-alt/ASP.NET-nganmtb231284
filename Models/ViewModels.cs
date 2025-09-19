namespace CafeWebsite.Models
{
    public class HomeViewModel
    {
        public List<Product> FeaturedProducts { get; set; } = new();
        public Dictionary<ProductCategory, int> CategoryCounts { get; set; } = new();
        public int TotalProducts { get; set; }
    }
    
    public class MenuViewModel
    {
        public List<Product> Products { get; set; } = new();
        public ProductCategory? SelectedCategory { get; set; }
        public string? SearchTerm { get; set; }
    }
    
    public class CartViewModel
    {
        public List<CartItem> Items { get; set; } = new();
        public decimal Total => Items.Sum(i => i.Total);
        public int ItemCount => Items.Sum(i => i.Quantity);
    }
    
    public class OrderViewModel
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string? CustomerEmail { get; set; }
        public string CustomerAddress { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? PromotionCode { get; set; }
        public List<CartItem> Items { get; set; } = new();
        public decimal Subtotal => Items.Sum(i => i.Total);
        public decimal DiscountAmount { get; set; } = 0;
        public decimal Total => Subtotal - DiscountAmount;
    }
    
    public class AdminDashboardViewModel
    {
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public decimal TodayRevenue { get; set; }
        public int TodayOrders { get; set; }
        public List<Product> PopularProducts { get; set; } = new();
        public List<Order> RecentOrders { get; set; } = new();
    }
}