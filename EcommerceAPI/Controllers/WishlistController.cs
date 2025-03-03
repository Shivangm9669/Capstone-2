

using EcommerceAPI.DTOs;
using EcommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<WishlistDto>> GetWishlist(int userId)
        {
            var wishlist = await _wishlistService.GetWishlistByUserIdAsync(userId);
            return Ok(wishlist);
        }

        [HttpPut("{wishlistId}")]
        [Authorize]
        public async Task<IActionResult> UpdateWishlist(int wishlistId, [FromBody] int productId)
        {
            var result = await _wishlistService.UpdateWishlistAsync(wishlistId, productId);
            if (!result) return NotFound("Wishlist not found or update failed.");

            return Ok("Wishlist updated successfully.");
        }

    }
}
