using KBDTypeServer.Application.Services.PaymentService;
using KBDTypeServer.Infrastructure.Repositories.OrderRepositories;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace KBDTypeServer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly IOrderRepository _orderRepository;
    private readonly string _webhookSecret;

    public PaymentController(IPaymentService paymentService, IOrderRepository orderRepository, IConfiguration configuration)
    {
        _paymentService = paymentService;
        _orderRepository = orderRepository;
        // Важливо: додайте "Stripe:WebhookSecret" у ваш файл конфігурації (appsettings.json)
        _webhookSecret = configuration["Stripe:WebhookSecret"]!;
    }

    [HttpPost("create-checkout-session")]
    public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest request)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, new CancellationToken());
        if (order == null)
        {
            return NotFound($"Order with ID {request.OrderId} not found.");
        }

        var session = await _paymentService.CreateCheckoutSessionAsync(order);
        
        // Повертаємо URL сесії, на який клієнт має бути перенаправлений
        return Ok(new { url = session.Url });
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        try
        {
            Event stripeEvent;

            // Якщо WebhookSecret не налаштовано (для локальної розробки),
            // ми пропускаємо перевірку підпису.
            // УВАГА: В продакшені WebhookSecret ОБОВ'ЯЗКОВИЙ для безпеки.
            if (string.IsNullOrEmpty(_webhookSecret))
            {
                stripeEvent = EventUtility.ParseEvent(json);
            }
            else
            {
                stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], _webhookSecret);
            }

            await _paymentService.HandleStripeEvent(stripeEvent);

            return Ok();
        }
        catch (StripeException e)
        {
            // Помилка валідації підпису або інша помилка Stripe
            return BadRequest(new { error = e.Message });
        }
        catch (Exception e)
        {
            // Інші можливі помилки
            return StatusCode(500, new { error = e.Message });
        }
    }
}

// Простий DTO для запиту на створення сесії
public class CreateCheckoutSessionRequest
{
    public int OrderId { get; set; }
}