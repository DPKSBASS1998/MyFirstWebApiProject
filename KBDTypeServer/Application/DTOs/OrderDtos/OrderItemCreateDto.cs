using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KBDTypeServer.Application.DTOs.OrderDtos
{
    public class OrderItemCreateDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
    }
}