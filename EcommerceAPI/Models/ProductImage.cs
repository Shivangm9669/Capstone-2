using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }  // Navigation property

        public string ImageUrl { get; set; } = string.Empty;
    }
}
