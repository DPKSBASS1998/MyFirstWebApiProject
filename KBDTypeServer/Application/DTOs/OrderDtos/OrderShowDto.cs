using KBDTypeServer.Domain.Enums;

namespace KBDTypeServer.Application.DTOs.OrderDtos
{
    public class OrderShowDto
    {
        public int? UserId { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string? Apartment { get; set; }
        public string PostalCode { get; set; }
        public string? Comment { get; set; }
        public List<OrderItemShowDto> Items { get; set; }
        public string? PaymentId { get; set; }
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
   
    }
}
