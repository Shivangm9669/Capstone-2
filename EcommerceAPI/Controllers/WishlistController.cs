using EcommerceAPI.Models;
using EcommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Wishlist>>> GetWishlist(int userId)
        {
            var wishlist = await _wishlistService.GetWishlist(userId);
            if (wishlist == null || !wishlist.Any())
            {
                return NotFound();
            }
            return Ok(wishlist);
        }

        [HttpPost("{userId}/add")]
        public async Task<IActionResult> AddToWishlist(int userId, [FromBody] int productId)
        {
            var result = await _wishlistService.AddorUpdateWishlist(userId, productId, true);
            return result;
        }

        [HttpDelete("{userId}/remove/{productId}")]
        public async Task<IActionResult> RemoveFromWishlist(int userId, int productId)
        {
            var result = await _wishlistService.AddorUpdateWishlist(userId, productId, false);
            return result;
        }
    }
}