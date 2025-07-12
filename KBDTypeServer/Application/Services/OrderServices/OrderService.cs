using KBDTypeServer.Domain.Entities.OrderEntity;
using KBDTypeServer.Domain.Factories;
using KBDTypeServer.Application.DTOs.OrderDtos;
using AutoMapper;
using KBDTypeServer.Domain.Enums;
using KBDTypeServer.Infrastructure.Repositories.OrderRepositories;
using KBDTypeServer.Domain.ValueObjects;

namespace KBDTypeServer.Application.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        public OrderService(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Mapper cannot be null");
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository), "Order repository cannot be null");
        }
        public async Task AddAsync(OrderCreateDto order, int userId, CancellationToken cancellationToken)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null");
            }
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be greater than zero");
            }
            var orderInitData = _mapper.Map<OrderInitData>(order);
            if (orderInitData == null)
            {
                throw new InvalidOperationException("Failed to map Order to OrderInitData");
            }
            var newOrder = OrderFactory.CreateOrder(orderInitData, userId);
            var result = await _orderRepository.AddAsync(newOrder, cancellationToken);
            if (result == null)
            {
                throw new InvalidOperationException("Failed to add order to the repository");
            }
            if (result.Id <= 0)
            {
                throw new InvalidOperationException("Order ID must be greater than zero after adding to the repository");
            }
            result.SetStatus(OrderStatus.WaitingForPayment);
            _orderRepository.UpdateAsync(result, new CancellationToken());
        }

        public Task<bool> DeleteAsync(OrderShowDto order, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderShowDto?>> GetAllByUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be greater than zero");
            }
            var orders = await _orderRepository.GetAllByUserIdAsync(userId, cancellationToken);
            if (orders == null)
            {
                throw new InvalidOperationException("No orders found for the specified user ID");
            }
            
            return _mapper.Map<List<OrderShowDto>>(orders);

        }

        public Task<OrderShowDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OrderShowDto?> UpdateAsync(OrderShowDto order, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
