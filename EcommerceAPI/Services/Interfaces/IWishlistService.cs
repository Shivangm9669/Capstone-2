using EcommerceAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceAPI.Services.Interfaces
{
    public interface IWishlistService
    {
        Task<IEnumerable<Wishlist>> GetAllWishlistsAsync();
        Task<Wishlist> GetWishlistByIdAsync(int id);
        Task<Wishlist> CreateWishlistAsync(Wishlist wishlist);
        Task<Wishlist> UpdateWishlistAsync(Wishlist wishlist);
        Task<bool> DeleteWishlistAsync(int id);
    }
}
