
namespace EcommerceAPI.DTOs
{
    public class WishlistDto
    {
        public int WishlistId { get; set; }
        public int UserId { get; set; }
        public List<ProductDto> Products { get; set; } = new();
    }
}
