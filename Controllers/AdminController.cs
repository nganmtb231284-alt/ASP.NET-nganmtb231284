using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeWebsite.Data;
using CafeWebsite.Models;

namespace CafeWebsite.Controllers
{
    public class AdminController : Controller
    {
        private readonly CafeDbContext _context;

        public AdminController(CafeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            var today = DateTime.Today;
            
            var viewModel = new AdminDashboardViewModel
            {
                TotalRevenue = await _context.Orders
                    .Where(o => o.Status == OrderStatus.Completed)
                    .SumAsync(o => o.Total),
                    
                TotalOrders = await _context.Orders.CountAsync(),
                
                TodayRevenue = await _context.Orders
                    .Where(o => o.CreatedAt.Date == today && o.Status == OrderStatus.Completed)
                    .SumAsync(o => o.Total),
                    
                TodayOrders = await _context.Orders
                    .Where(o => o.CreatedAt.Date == today)
                    .CountAsync(),
                    
                PopularProducts = await _context.Products.Take(5).ToListAsync(),
                RecentOrders = await _context.Orders
                    .Include(o => o.Items)
                    .OrderByDescending(o => o.CreatedAt)
                    .Take(10)
                    .ToListAsync()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Products()
        {
            var products = await _context.Products
                .OrderBy(p => p.Category)
                .ThenBy(p => p.Name)
                .ToListAsync();
                
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid().ToString();
                product.CreatedAt = DateTime.Now;
                
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                
                TempData["Success"] = "Sản phẩm đã được thêm thành công";
            }
            else
            {
                TempData["Error"] = "Có lỗi xảy ra khi thêm sản phẩm";
            }
            
            return RedirectToAction("Products");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                
                return Json(new { success = true, message = "Cập nhật thành công" });
            }
            
            return Json(new { success = false, message = "Dữ liệu không hợp lệ" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                var hasOrders = await _context.OrderItems.AnyAsync(oi => oi.ProductId == id);
                
                if (hasOrders)
                {
                    return Json(new { success = false, message = "Không thể xóa sản phẩm đã có đơn hàng" });
                }
                
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                
                return Json(new { success = true, message = "Xóa thành công" });
            }
            
            return Json(new { success = false, message = "Sản phẩm không tồn tại" });
        }

        public async Task<IActionResult> Orders(OrderStatus? status)
        {
            IQueryable<Order> query = _context.Orders.Include(o => o.Items);
            
            if (status.HasValue)
            {
                query = query.Where(o => o.Status == status.Value);
            }
            
            var orders = await query
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
                
            ViewBag.SelectedStatus = status;
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(string orderId, OrderStatus newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = newStatus;
                order.UpdatedAt = DateTime.Now;
                
                await _context.SaveChangesAsync();
                
                return Json(new { success = true, message = "Cập nhật trạng thái thành công" });
            }
            
            return Json(new { success = false, message = "Đơn hàng không tồn tại" });
        }
    }
}