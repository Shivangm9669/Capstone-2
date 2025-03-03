using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        
        [ForeignKey("User")]
        public int UserId { get; set; }
        
        public float Rating { get; set; }
        
        public string Comment { get; set; } = string.Empty;
    }
}
