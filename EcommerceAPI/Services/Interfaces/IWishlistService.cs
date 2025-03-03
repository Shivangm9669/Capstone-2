using EcommerceAPI.Models;

namespace EcommerceAPI.Services.Interfaces
{
    public interface IWishlistService
    {
        Task<IEnumerable<Wishlist>> GetWishlistByUserIdAsync(int userId);
        Task<bool> AddToWishlistAsync(int userId, int productId);
        Task<bool> UpdateWishlistAsync(int wishlistId, int productId);
        Task<bool> RemoveFromWishlistAsync(int userId, int productId);
    }
}
