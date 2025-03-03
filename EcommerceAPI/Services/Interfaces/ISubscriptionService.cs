using EcommerceAPI.Models;

namespace EcommerceAPI.Services.Interfaces
{
    public interface ISubscriptionService
    {
        Task<Subscription?> GetSubscriptionByUserIdAsync(int userId);
        Task<bool> SubscribeAsync(Subscription subscription);
        Task<bool> CancelSubscriptionAsync(int userId);
    }
}
