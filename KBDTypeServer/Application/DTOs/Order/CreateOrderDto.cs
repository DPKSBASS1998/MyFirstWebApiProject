// Application/DTOs/Order/CreateOrderDto.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KBDTypeServer.Application.DTOs.Order
{
    public class CreateOrderDto
    {
        [Required]
        public ProfileDto Profile { get; set; }

        [Required]
        public AddressDto Address { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Потрібно хоча б один товар у замовленні")]
        public List<OrderItemDto> Items { get; set; }

        [MaxLength(500)]
        public string? Comment { get; set; }
    }
}
