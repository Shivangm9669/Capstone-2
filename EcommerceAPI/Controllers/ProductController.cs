using EcommerceAPI.DTOs;
using EcommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();

            // Map products to DTOs including image URLs
            var productDtos = products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Rating = p.Rating,
                CategoryId = p.CategoryId,
                ImageUrls = p.ProductImages?.Select(img => img.ImageUrl).ToList() ?? new List<string>()
            });

            return Ok(productDtos);
        }

    }
}
