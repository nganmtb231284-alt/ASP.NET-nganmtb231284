using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeWebsite.Data;
using CafeWebsite.Models;
using CafeWebsite.Services;

namespace CafeWebsite.Controllers
{
    public class CartController : Controller
    {
        private readonly CafeDbContext _context;
        private readonly CartService _cartService;

        public CartController(CafeDbContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var cartItems = _cartService.GetCartItems();
            
            foreach (var item in cartItems)
            {
                item.Product = await _context.Products.FindAsync(item.ProductId);
            }

            var viewModel = new CartViewModel
            {
                Items = cartItems
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateQuantity(string productId, ProductSize size, int quantity)
        {
            _cartService.UpdateQuantity(productId, size, quantity);
            return Json(new { success = true, cartCount = _cartService.GetCartItemCount(), cartTotal = _cartService.GetCartTotal() });
        }

        [HttpPost]
        public IActionResult RemoveItem(string productId, ProductSize size)
        {
            _cartService.RemoveFromCart(productId, size);
            return Json(new { success = true, cartCount = _cartService.GetCartItemCount(), cartTotal = _cartService.GetCartTotal() });
        }

        [HttpPost]
        public IActionResult Clear()
        {
            _cartService.ClearCart();
            return Json(new { success = true });
        }

        public IActionResult Checkout()
        {
            var cartItems = _cartService.GetCartItems();
            if (!cartItems.Any())
            {
                return RedirectToAction("Index", "Menu");
            }

            var viewModel = new OrderViewModel
            {
                Items = cartItems
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Items = _cartService.GetCartItems();
                return View("Checkout", model);
            }

            var cartItems = _cartService.GetCartItems();
            if (!cartItems.Any())
            {
                return RedirectToAction("Index", "Menu");
            }

            var subtotal = cartItems.Sum(x => x.Total);
            var discountAmount = 0m;
            Promotion? promotion = null;
            
            if (!string.IsNullOrEmpty(model.PromotionCode))
            {
                promotion = await _context.Promotions
                    .FirstOrDefaultAsync(p => p.Code == model.PromotionCode && p.IsActive);
                    
                if (promotion != null)
                {
                    switch (promotion.Type)
                    {
                        case PromotionType.Percentage:
                            discountAmount = subtotal * promotion.Value / 100;
                            break;
                        case PromotionType.FixedAmount:
                            discountAmount = promotion.Value;
                            break;
                        case PromotionType.FreeShip:
                            discountAmount = 20000;
                            break;
                    }
                    
                    promotion.UsedCount++;
                    _context.Promotions.Update(promotion);
                }
            }

            var order = new Order
            {
                UserId = HttpContext.Session.GetString("UserId"),
                CustomerName = model.CustomerName,
                CustomerPhone = model.CustomerPhone,
                CustomerEmail = model.CustomerEmail,
                CustomerAddress = model.CustomerAddress,
                Notes = model.Notes,
                PaymentMethod = model.PaymentMethod,
                Subtotal = subtotal,
                DiscountAmount = discountAmount,
                Total = subtotal - discountAmount,
                PromotionCode = model.PromotionCode,
                PromotionId = promotion?.Id
            };

            foreach (var cartItem in cartItems)
            {
                var product = await _context.Products.FindAsync(cartItem.ProductId);
                if (product != null)
                {
                    order.Items.Add(new OrderItem
                    {
                        ProductId = cartItem.ProductId,
                        ProductName = product.Name,
                        Size = cartItem.Size,
                        Quantity = cartItem.Quantity,
                        Price = product.GetPriceBySize(cartItem.Size),
                        Toppings = cartItem.Toppings
                    });
                }
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            _cartService.ClearCart();

            return RedirectToAction("OrderSuccess", new { orderId = order.Id });
        }

        public async Task<IActionResult> OrderSuccess(string orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}