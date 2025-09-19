using System.ComponentModel.DataAnnotations;

namespace CafeWebsite.Models
{
    public class Promotion
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string Code { get; set; } = string.Empty;
        
        public PromotionType Type { get; set; }
        public decimal Value { get; set; }
        public decimal MinOrderAmount { get; set; } = 0;
        public int MaxUses { get; set; } = 0;
        public int UsedCount { get; set; } = 0;
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
    
    public enum PromotionType
    {
        Percentage,  // Giảm %
        FixedAmount, // Giảm số tiền cố định
        FreeShip     // Miễn phí ship
    }
    
    public class PromotionViewModel
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public string Code { get; set; } = string.Empty;
        
        [Required]
        public PromotionType Type { get; set; }
        
        [Required]
        public decimal Value { get; set; }
        
        public decimal MinOrderAmount { get; set; }
        public int MaxUses { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
    }
}