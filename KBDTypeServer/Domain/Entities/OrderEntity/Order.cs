using System.ComponentModel.DataAnnotations;
using KBDTypeServer.Domain.Entities.UserEntity;
using KBDTypeServer.Domain.Enums;

namespace KBDTypeServer.Domain.Entities.OrderEntity
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Building { get; set; }
        public string? Apartment { get; set; }
        [Required]
        public string PostalCode { get; set; }
        public List<OrderItem> Items { get; set; } = new();
        public string? Comment { get; set; }

        // --- Поля для інтеграції зі Stripe (обидва обов'язкові для зберігання) ---
        public string? PaymentId { get; set; }
        
        // --------------------------------------------------------------------

        public string? TrackingNumber { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime? ShippedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public DateTime? RefundedAt { get; set; }

        private static readonly Dictionary<OrderStatus, List<OrderStatus>> AllowedTransitions = new()
        {
            [OrderStatus.Created] = new() { OrderStatus.WaitingForPayment, OrderStatus.Cancelled },
            [OrderStatus.WaitingForPayment] = new() { OrderStatus.Paid, OrderStatus.Cancelled },
            [OrderStatus.Paid] = new() { OrderStatus.Shipped, OrderStatus.Cancelled },
            [OrderStatus.WhatingForShipping] = new() { OrderStatus.Shipped, OrderStatus.Cancelled },
            [OrderStatus.Shipped] = new() { OrderStatus.Delivered },
            [OrderStatus.Delivered] = new() {OrderStatus.WaitingForRefund, OrderStatus.Success},
            [OrderStatus.WaitingForRefund] = new() { OrderStatus.Refunded},
            [OrderStatus.Cancelled] = new() { OrderStatus.WaitingForRefund },
            [OrderStatus.Refunded] = new() { OrderStatus.Failed }
        };

        public void SetStatus(OrderStatus newStatus, bool force = false)
        {
            if (Status == newStatus) return;
            if (!force && !AllowedTransitions[Status].Contains(newStatus))
                throw new InvalidOperationException($"Cannot change status from {Status} to {newStatus}.");

            var oldStatus = Status;
            Status = newStatus;
            var now = DateTime.UtcNow;
            string? extra = null;

            switch (newStatus)
            {
                case OrderStatus.Paid:
                    PaidAt = now;
                    // ВИПРАВЛЕНО: Використовуємо PaymentIntentId для логування
                    extra = $"PaymentIntentId: {PaymentId}";
                    break;
                case OrderStatus.Shipped:
                    ShippedAt = now;
                    extra = $"Package shipped. TrackingNumber: {TrackingNumber}";
                    break;
                case OrderStatus.Delivered:
                    DeliveredAt = now;
                    extra = "Your package is waiting for you)";
                    break;
                case OrderStatus.Cancelled:
                    CancelledAt = now;
                    break;
                case OrderStatus.Refunded:
                    RefundedAt = now;
                    extra = $"Refunded successfully: {now}";
                    break;
                // Інші статуси не потребують додаткової інформації
                case OrderStatus.WaitingForPayment:
                case OrderStatus.WhatingForShipping:
                case OrderStatus.WaitingForRefund:
                case OrderStatus.Success:
                case OrderStatus.Failed:
                    break;
            }

            OnStatusChanged?.Invoke(this, newStatus, extra);
        }

        public event Action<Order, OrderStatus, string?>? OnStatusChanged;

        public void WaitForPayment() => SetStatus(OrderStatus.WaitingForPayment);

        public void Pay()
        {
            // ВИДАЛЕНО: Логіка з PaymentId тут не потрібна.
            // Статус Paid встановлюється через вебхук, який викликає SetStatus.
            // Цей метод може бути застарілим або використовуватися для ручного підтвердження.
            SetStatus(OrderStatus.Paid);
        }

        public void WaitForShipping() => SetStatus(OrderStatus.WhatingForShipping);
        public void Ship()
        {
            SetStatus(OrderStatus.Shipped);
            TrackingNumber ??= Guid.NewGuid().ToString();
        }
        public void Deliver() => SetStatus(OrderStatus.Delivered);
        public void WaitForRefund() => SetStatus(OrderStatus.WaitingForRefund);
        public void Refund() => SetStatus(OrderStatus.Refunded);
        public void MarkAsSuccess() => SetStatus(OrderStatus.Success);
        public void MarkAsFailed() => SetStatus(OrderStatus.Failed);
        public void Cancel() => SetStatus(OrderStatus.Cancelled);
    }
}