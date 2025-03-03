namespace EcommerceAPI.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Rating { get; set; }
        public int CategoryId { get; set; }
        public List<string>? ImageUrls { get; set; }
    }
}
