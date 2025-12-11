using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController: ControllerBase
    {
        private readonly MongoDbContext _ctx;

        public OrdersController(MongoDbContext ctx) => _ctx = ctx;

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(string bookId, int quantity)
        {
            var userId = User.FindFirst("userId")?.Value;
            if (userId == null) return Unauthorized();

            var order = new Order
            {
                UserId = userId,
                BookId = bookId,
                Quantity = quantity
            };

            await _ctx.Orders.InsertOneAsync(order);
            return Ok(order);
        }
    }
}
