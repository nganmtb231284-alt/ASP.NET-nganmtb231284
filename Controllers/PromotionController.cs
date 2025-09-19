using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeWebsite.Data;
using CafeWebsite.Models;

namespace CafeWebsite.Controllers
{
    public class PromotionController : Controller
    {
        private readonly CafeDbContext _context;

        public PromotionController(CafeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var promotions = await _context.Promotions
                .Where(p => p.IsActive && p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return View(promotions);
        }

        [HttpPost]
        public async Task<IActionResult> ValidateCode(string code, decimal orderTotal)
        {
            var promotion = await _context.Promotions
                .FirstOrDefaultAsync(p => p.Code == code && p.IsActive 
                    && p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now
                    && (p.MaxUses == 0 || p.UsedCount < p.MaxUses));

            if (promotion == null)
            {
                return Json(new { success = false, message = "Mã khuyến mãi không hợp lệ hoặc đã hết hạn" });
            }

            if (orderTotal < promotion.MinOrderAmount)
            {
                return Json(new { 
                    success = false, 
                    message = $"Đơn hàng tối thiểu {promotion.MinOrderAmount:N0}đ để sử dụng mã này" 
                });
            }

            decimal discountAmount = 0;
            switch (promotion.Type)
            {
                case PromotionType.Percentage:
                    discountAmount = orderTotal * promotion.Value / 100;
                    break;
                case PromotionType.FixedAmount:
                    discountAmount = promotion.Value;
                    break;
                case PromotionType.FreeShip:
                    discountAmount = 20000; // Phí ship cố định
                    break;
            }

            return Json(new { 
                success = true, 
                discountAmount = discountAmount,
                promotionId = promotion.Id,
                message = $"Áp dụng thành công! Giảm {discountAmount:N0}đ"
            });
        }
    }
}