using System.ComponentModel.DataAnnotations;
using KBDTypeServer.Domain.Entities.UserEntity;
using KBDTypeServer.Domain.Enums;

namespace KBDTypeServer.Domain.Entities.OrderEntity
{
    public class Order
    {
        /// <summary>
        /// Unique identifier for the order.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Unique identifier for the user who placed the order.
        /// </summary>
        public string? UserId { get; set; }
        public User? User { get; set; }

        /// <summary>
        /// Data about user who placed the order.
        /// </summary>
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Adress details for the order.
        /// </summary>
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

        /// <summary>
        /// List of items in the order, each represented by an OrderItem entity.
        /// </summary>
        public List<OrderItem> Items { get; set; } = new();

        /// <summary>
        /// Optional comment or note about the order.
        /// </summary>
        public string? Comment { get; set; }
        public string? PaymentId { get; set; }

        /// <summary>
        /// Freight Tracking Link (TrackingNumber) for the order, if applicable.
        /// </summary>
        public string? TrackingNumber { get; set; }
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Price before any discounts or shipping costs.
        /// </summary>
        public decimal Subtotal { get; set; }
        // Якщо в майбутньому буде логіка для shippingPrice і discount, то можна додати їх як властивості
        //public decimal ShippingPrice { get; set; }
        //public decimal Discount { get; set; }

        /// <summary>
        /// Total price of the order, including all items, discounts, and shipping costs.
        /// </summary>
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime? ShippedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public DateTime? CancelledAt { get; set; }

        public DateTime? RefundedAt { get; set; } // Додано для відстеження часу повернення

        private static readonly Dictionary<OrderStatus, List<OrderStatus>> AllowedTransitions = new()
        {
            [OrderStatus.Created] = new() { OrderStatus.WaitingForPayment, OrderStatus.Cancelled },
            [OrderStatus.WaitingForPayment] = new() { OrderStatus.Paid, OrderStatus.Cancelled },
            [OrderStatus.Paid] = new() { OrderStatus.Shipped, OrderStatus.Cancelled },
            [OrderStatus.WhatingForShipping] = new() { OrderStatus.Shipped, OrderStatus.Cancelled },
            [OrderStatus.Shipped] = new() { OrderStatus.Delivered },
            [OrderStatus.Delivered] = new() {OrderStatus.WaitingForRefund, OrderStatus.Success},// // Додано можливість повернення після доставки
            [OrderStatus.WaitingForRefund] = new() { OrderStatus.Refunded}, // Додано можливість повернення
            [OrderStatus.Cancelled] = new() { OrderStatus.WaitingForRefund }, // Не можна змінити статус після скасування
            [OrderStatus.Refunded] = new() { OrderStatus.Failed } // Повернення завершено
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
                case OrderStatus.WaitingForPayment:
                    // WaitingForPayment does not have a specific timestamp, but can be used for logging
                    break;
                case OrderStatus.Paid:
                    PaidAt = now;
                    extra = $"PaymentId: {PaymentId}";
                    break;
                case OrderStatus.WhatingForShipping:
                    // WhatingForShipping does not have a specific timestamp, but can be used for logging
                    break;
                case OrderStatus.Shipped:
                    ShippedAt = now;
                    extra = $"Package shipped. TrackingNumber: {TrackingNumber}";
                    break;
                case OrderStatus.Delivered:
                    DeliveredAt = now;
                    extra = $"Your package is waiting for you)";
                    break;
                case OrderStatus.Cancelled:
                    CancelledAt = now;
                    break;
                case OrderStatus.WaitingForRefund:
                    // WaitingForRefund does not have a specific timestamp, but can be used for logging
                    break;
                case OrderStatus.Refunded:
                    RefundedAt = now;
                    extra = $"Refunded sucsesfull: {now}";
                    break;
                case OrderStatus.Success:
                    // Success does not have a specific timestamp, but can be used for logging
                    break;
                case OrderStatus.Failed:
                    // Failed does not have a specific timestamp, but can be used for logging
                    break;

            }

            OnStatusChanged?.Invoke(this, newStatus, extra);
        }


        /// <summary>
        /// При зміні статусу замовлення викликається подія.
        /// </summary>
        public event Action<Order, OrderStatus, string?>? OnStatusChanged;

        public void WaitForPayment()
        {
            SetStatus(OrderStatus.WaitingForPayment);
        }

        public void Pay()
        {
            SetStatus(OrderStatus.Paid);
            // PaymentId can be generated or assigned here if needed
            PaymentId ??= Guid.NewGuid().ToString();
        }

        public void WaitForShipping()
        {
            SetStatus(OrderStatus.WhatingForShipping);
        }

        public void Ship()
        {
            SetStatus(OrderStatus.Shipped);
            // FTL can be generated or assigned here if needed
            TrackingNumber ??= Guid.NewGuid().ToString(); // Example of generating a tracking link
        }

        public void Deliver()
        {
            SetStatus(OrderStatus.Delivered);
        }

        public void WaitForRefund()
        {
            SetStatus(OrderStatus.WaitingForRefund);
        }

        public void Refund()
        {
            SetStatus(OrderStatus.Refunded);
        }

        public void MarkAsSuccess()
        {
            SetStatus(OrderStatus.Success);
        }
        public void MarkAsFailed()
        {
            SetStatus(OrderStatus.Failed);
        }

        public void Cancel()
        {
            SetStatus(OrderStatus.Cancelled);
        }
    }

}
