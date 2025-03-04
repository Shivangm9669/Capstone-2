using EcommerceAPI.Data;
using EcommerceAPI.Models;
using EcommerceAPI.Services.Interfaces;
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

        public async Task<IEnumerable<Wishlist>> GetWishlistByUserIdAsync(int userId) =>
            await _context.Wishlists.Where(w => w.UserId == userId).ToListAsync();

        public async Task<bool> AddToWishlistAsync(int userId, int productId)
        {
            if (await _context.Wishlists.AnyAsync(w => w.UserId == userId && w.ProductId == productId))
                return false; // Already in wishlist

            var wishlistItem = new Wishlist
            {
                UserId = userId,
                ProductId = productId
            };

            await _context.Wishlists.AddAsync(wishlistItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveFromWishlistAsync(int userId, int productId)
        {
            var wishlistItem = await _context.Wishlists.FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);
            if (wishlistItem == null) return false;

            _context.Wishlists.Remove(wishlistItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateWishlistAsync(int wishlistId, int productId)
        {
            var wishlist = await _context.Wishlists.FirstOrDefaultAsync(w => w.WishlistId == wishlistId);
            if (wishlist == null) return false;

            wishlist.ProductId = productId;  // Update the product in wishlist
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Wishlist>> GetAllWishlistsAsync()
        {
            return await _context.Wishlists.ToListAsync();
        }

        public async Task<Wishlist> GetWishlistByIdAsync(int id)
        {
            return await _context.Wishlists.FindAsync(id) ?? throw new InvalidOperationException("Wishlist not found");
        }

        public async Task<Wishlist> CreateWishlistAsync(Wishlist wishlist)
        {
            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();
            return wishlist;
        }

        public async Task<Wishlist> UpdateWishlistAsync(Wishlist wishlist)
        {
            _context.Wishlists.Update(wishlist);
            await _context.SaveChangesAsync();
            return wishlist;
        }

        public async Task<bool> DeleteWishlistAsync(int id)
        {
            var wishlist = await _context.Wishlists.FindAsync(id);
            if (wishlist == null)
            {
                return false;
            }

            _context.Wishlists.Remove(wishlist);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
