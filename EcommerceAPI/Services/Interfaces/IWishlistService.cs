using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Services.Interfaces
{
    public interface IWishlistService
    {
        Task<IActionResult> AddorUpdateWishlist(int userId, int productId, bool isAdding);
        Task<List<Wishlist>> GetWishlist(int userId);
    }
}
