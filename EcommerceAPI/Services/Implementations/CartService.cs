using EcommerceAPI.Data;
using EcommerceAPI.Models;
using EcommerceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using EcommerceAPI.DTOs;

namespace EcommerceAPI.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly EcommerceDbContext _context;

        public CartService(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetCartByUserIdAsync(int userId) =>
            await _context.Carts.Include(c => c.UserId).FirstOrDefaultAsync(c => c.UserId == userId);

        public async Task<bool> AddToCartAsync(int userId, int productId, int quantity)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart == null) return false;

            var cartItem = new CartItem
            {
                CartId = cart.CartId,
                ProductId = productId,
                Quantity = quantity
            };

            await _context.CartItems.AddAsync(cartItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveFromCartAsync(int userId, int productId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart == null) return false;

            var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.ProductId == productId);
            if (cartItem == null) return false;

            _context.CartItems.Remove(cartItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ClearCartAsync(int userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart == null) return false;

            var cartItems = _context.CartItems.Where(ci => ci.CartId == cart.CartId);
            _context.CartItems.RemoveRange(cartItems);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<CartDto> GetCartDetailsAsync(int userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart == null) return null;

            var cartItems = await _context.CartItems
                .Where(ci => ci.CartId == cart.CartId)
                .Select(ci => new CartItemDto
                {
                    ProductId = ci.ProductId,
                    ProductName = _context.Products.FirstOrDefault(p => p.ProductId == ci.ProductId).Name,
                    Price = _context.Products.FirstOrDefault(p => p.ProductId == ci.ProductId).Price,
                    Quantity = ci.Quantity
                }).ToListAsync();

            return new CartDto
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                Items = cartItems,
                TotalPrice = cartItems.Sum(ci => ci.Price * ci.Quantity)
            };
        }
    }
}
