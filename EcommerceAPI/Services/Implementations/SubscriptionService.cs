using EcommerceAPI.Data;
using EcommerceAPI.Models;
using EcommerceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Services.Implementations
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly EcommerceDbContext _context;

        public SubscriptionService(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Subscription?> GetSubscriptionByUserIdAsync(int userId) =>
            await _context.Subscriptions.FirstOrDefaultAsync(s => s.UserId == userId);

        public async Task<bool> SubscribeAsync(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CancelSubscriptionAsync(int userId)
        {
            var subscription = await GetSubscriptionByUserIdAsync(userId);
            if (subscription == null) return false;

            _context.Subscriptions.Remove(subscription);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
