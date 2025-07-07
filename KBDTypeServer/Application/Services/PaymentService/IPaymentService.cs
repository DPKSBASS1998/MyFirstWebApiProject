// KBDTypeServer/Application/Services/PaymentService/IPaymentService.cs
using Stripe;
using Stripe.Checkout;
using DomainOrder = KBDTypeServer.Domain.Entities.OrderEntity.Order;

namespace KBDTypeServer.Application.Services.PaymentService;

public interface IPaymentService
{
    Task<Session> CreateCheckoutSessionAsync(DomainOrder order);
    Task HandleStripeEvent(Event stripeEvent);
}