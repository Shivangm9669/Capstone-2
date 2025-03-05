using EcommerceAPI.Data;
using EcommerceAPI.Models;
using EcommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EcommerceAPI.Services.Implementations
{
    public class WishlistService : IWishlistService
    {
        private readonly EcommerceDbContext _context;

        public WishlistService(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddorUpdateWishlist(int userId, int productId, bool isAdding)
        {
            var wishlistItem = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);

            if (isAdding)
            {
                if (wishlistItem == null)
                {
                    wishlistItem = new Wishlist
                    {
                        UserId = userId,
                        ProductId = productId
                    };

                    _context.Wishlists.Add(wishlistItem);
                    await _context.SaveChangesAsync();
                }
                return new OkObjectResult(wishlistItem);
            }
            else
            {
                if (wishlistItem != null)
                {
                    _context.Wishlists.Remove(wishlistItem);
                    await _context.SaveChangesAsync();
                }
                return new OkResult();
            }
        }

        public async Task<List<Wishlist>> GetWishlist(int userId)
        {
            var wishlist = await _context.Wishlists
                .Where(w => w.UserId == userId)
                .ToListAsync();

            return wishlist;
        }
    }
}