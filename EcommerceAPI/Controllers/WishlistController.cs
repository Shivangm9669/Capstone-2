
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wishlist>>> GetAllWishlists()
        {
            var wishlists = await _wishlistService.GetAllWishlistsAsync();
            return Ok(wishlists);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Wishlist>> GetWishlistById(int id)
        {
            var wishlist = await _wishlistService.GetWishlistByIdAsync(id);
            if (wishlist == null)
            {
                return NotFound();
            }
            return Ok(wishlist);
        }

        [HttpPost]
        public async Task<ActionResult<Wishlist>> CreateWishlist(Wishlist wishlist)
        {
            var createdWishlist = await _wishlistService.CreateWishlistAsync(wishlist);
            return CreatedAtAction(nameof(GetWishlistById), new { id = createdWishlist.WishlistId }, createdWishlist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWishlist(int id, Wishlist wishlist)
        {
            if (id != wishlist.WishlistId)
            {
                return BadRequest();
            }

            await _wishlistService.UpdateWishlistAsync(wishlist);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishlist(int id)
        {
            var result = await _wishlistService.DeleteWishlistAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}