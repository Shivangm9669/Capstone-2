namespace EcommerceAPI.DTOs
{
    public class SubscriptionDto
    {
        public int UserId { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
