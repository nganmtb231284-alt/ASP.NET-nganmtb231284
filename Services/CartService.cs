using CafeWebsite.Models;
using System.Text.Json;

namespace CafeWebsite.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionKey = "Cart";
        
        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public List<CartItem> GetCartItems()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            var cartJson = session?.GetString(CartSessionKey);
            
            if (string.IsNullOrEmpty(cartJson))
                return new List<CartItem>();
                
            return JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }
        
        public void AddToCart(CartItem item)
        {
            var cartItems = GetCartItems();
            var existingItem = cartItems.FirstOrDefault(x => 
                x.ProductId == item.ProductId && x.Size == item.Size);
                
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cartItems.Add(item);
            }
            
            SaveCart(cartItems);
        }
        
        public void UpdateQuantity(string productId, ProductSize size, int quantity)
        {
            var cartItems = GetCartItems();
            var item = cartItems.FirstOrDefault(x => 
                x.ProductId == productId && x.Size == size);
                
            if (item != null)
            {
                if (quantity <= 0)
                {
                    cartItems.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }
                
                SaveCart(cartItems);
            }
        }
        
        public void RemoveFromCart(string productId, ProductSize size)
        {
            var cartItems = GetCartItems();
            var item = cartItems.FirstOrDefault(x => 
                x.ProductId == productId && x.Size == size);
                
            if (item != null)
            {
                cartItems.Remove(item);
                SaveCart(cartItems);
            }
        }
        
        public void ClearCart()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            session?.Remove(CartSessionKey);
        }
        
        public int GetCartItemCount()
        {
            return GetCartItems().Sum(x => x.Quantity);
        }
        
        public decimal GetCartTotal()
        {
            return GetCartItems().Sum(x => x.Total);
        }
        
        private void SaveCart(List<CartItem> cartItems)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            var cartJson = JsonSerializer.Serialize(cartItems);
            session?.SetString(CartSessionKey, cartJson);
        }
    }
}