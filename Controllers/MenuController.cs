using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeWebsite.Data;
using CafeWebsite.Models;
using CafeWebsite.Services;

namespace CafeWebsite.Controllers
{
    public class MenuController : Controller
    {
        private readonly CafeDbContext _context;
        private readonly CartService _cartService;

        public MenuController(CafeDbContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index(ProductCategory? category, string? search)
        {
            var query = _context.Products.Where(p => p.Available);

            if (category.HasValue)
            {
                query = query.Where(p => p.Category == category.Value);
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
            }

            var products = await query.OrderBy(p => p.Name).ToListAsync();

            var viewModel = new MenuViewModel
            {
                Products = products,
                SelectedCategory = category,
                SearchTerm = search
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(string productId, ProductSize size, int quantity = 1)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null || !product.Available)
            {
                return NotFound();
            }

            var cartItem = new CartItem
            {
                ProductId = productId,
                Product = product,
                Size = size,
                Quantity = quantity
            };

            _cartService.AddToCart(cartItem);

            return Json(new { success = true, cartCount = _cartService.GetCartItemCount() });
        }
    }
}