using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KBDTypeServer.Infrastructure;
using KBDTypeServer.Application;
using KBDTypeServer.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using KBDTypeServer.Domain.Entities.UserEntity;
using KBDTypeServer.Application.Services.OrderServices;
using KBDTypeServer.Application.DTOs.OrderDtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace KBDTypeServer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService), "Order service cannot be null");
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<OrderShowDto?>>> GetAll(CancellationToken cancellationToken)
        {
            var userId = GetСurrentUserId();
            if (userId == null)
            {
                return Unauthorized("User ID is required to retrieve orders.");
            }
            var orders = await _orderService.GetAllByUserIdAsync(userId.Value, cancellationToken);
            if (orders == null || !orders.Any())
            {
                return NotFound("No orders found for the current user.");
            }
            return Ok(orders);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderShowDto?>> Add(OrderCreateDto order, CancellationToken cancellationToken)
        {
            if (order == null)
            {
                return BadRequest("Order data is required.");
            }
            var userId = GetСurrentUserId();
            if (userId == null)
            {
                return Unauthorized("User ID is required to add an order.");
            }
            var UserId = userId.Value; // Ensure the order is associated with the current user
            var addedOrder = await _orderService.AddAsync(order, UserId, cancellationToken);
            if (addedOrder == null)
            {
                return BadRequest("Failed to add the order.");
            }
            return addedOrder; 
        }
    }
}
