// Application/DTOs/Order/OrderItemDto.cs
using System.ComponentModel.DataAnnotations;

namespace KBDTypeServer.Application.DTOs.Order
{
    public class OrderItemDto
    {
        /// <summary>Id товару</summary>
        [Required]
        public int ProductId { get; set; }

        /// <summary>Кількість</summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        /// <summary>Ціна за одиницю на момент замовлення</summary>
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive")]
        public decimal Price { get; set; }
    }
}
