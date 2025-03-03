using EcommerceAPI.DTOs;
using EcommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<SubscriptionDto>> GetSubscription(int userId)
        {
            var subscription = await _subscriptionService.GetSubscriptionByUserIdAsync(userId);
            return Ok(subscription);
        }
    }
}
