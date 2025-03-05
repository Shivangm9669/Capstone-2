using EcommerceAPI.DTOs;
using EcommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("{userId}/add")]
        public async Task<IActionResult> AddToCart(int userId, [FromBody] AddToCartDto addToCartDto)
        {
            var result = await _cartService.AddToCartAsync(userId, addToCartDto.ProductId, addToCartDto.Quantity);
            if (!result) return BadRequest("Failed to add item to cart.");

            return Ok("Item added to cart successfully.");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartDetails(int userId)
        {
            var cartDetails = await _cartService.GetCartDetailsAsync(userId);
            if (cartDetails == null) return NotFound("Cart not found.");

            return Ok(cartDetails);
        }
    }
}
