// KBDTypeServer/Application/Services/PaymentService/PaymentService.cs
using KBDTypeServer.Domain.Enums;
using KBDTypeServer.Infrastructure.Repositories.OrderRepositories;
using Stripe;
using Stripe.Checkout;
using DomainOrder = KBDTypeServer.Domain.Entities.OrderEntity.Order;

namespace KBDTypeServer.Application.Services.PaymentService;

public class PaymentService : IPaymentService
{
    private readonly IConfiguration _configuration;
    private readonly IOrderRepository _orderRepository;

    public PaymentService(IConfiguration configuration, IOrderRepository orderRepository)
    {
        _configuration = configuration;
        _orderRepository = orderRepository;
        StripeConfiguration.ApiKey = _configuration["Stripe:ApiKey"];
    }

    public async Task<Session> CreateCheckoutSessionAsync(DomainOrder order)
    {
        var lineItems = order.Items.Select(item => new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmount = (long)(item.Price * 100), // Ціна в копійках
                Currency = "uah",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = item.Product.Name,
                    Description = item.Product.Description
                },
            },
            Quantity = item.Quantity,
        }).ToList();

        var domain = "http://localhost:5173";

        var options = new SessionCreateOptions
        {
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = $"{domain}/success?session_id={{CHECKOUT_SESSION_ID}}",
            CancelUrl = $"{domain}/cancel",
            Metadata = new Dictionary<string, string>
            {
                { "order_id", order.Id.ToString() }
            }
        };

        var service = new SessionService();
        Session session = await service.CreateAsync(options);

        // Зберігаємо ID сесії в замовленні для зв'язку
        order.PaymentId = session.Id;
        await _orderRepository.UpdateAsync(order, new CancellationToken());

        return session;
    }

    public async Task HandleStripeEvent(Event stripeEvent)
    {
        // Ми слухаємо тільки одну подію: завершення сесії оплати
        if (stripeEvent.Type == "checkout.session.completed")        {
            var session = stripeEvent.Data.Object as Session;

            // Перевіряємо, чи оплата дійсно пройшла успішно
            if (session?.PaymentStatus == "paid")
            {
                // Витягуємо ID нашого замовлення з метаданих
                if (session.Metadata.TryGetValue("order_id", out var orderIdStr) &&
                    int.TryParse(orderIdStr, out var orderId))
                {
                    await FulfillOrder(orderId);
                }
                // Тут варто додати логування, якщо order_id не знайдено
            }
        }
    }

    private async Task FulfillOrder(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId, new CancellationToken());

        // Ця перевірка є критично важливою для надійності системи.
        // Вона захищає від повторної обробки вже оплаченого замовлення.
        if (order != null && order.Status != OrderStatus.Paid)
        {
            order.SetStatus(OrderStatus.Paid);
            await _orderRepository.UpdateAsync(order, new CancellationToken());
        }
    }
}