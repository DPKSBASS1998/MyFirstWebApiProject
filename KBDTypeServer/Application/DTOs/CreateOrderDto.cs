using System.ComponentModel.DataAnnotations;

namespace KBDTypeServer.Application.DTOs
{
    public class CreateOrderDto
    {
        [Required] public ProfileDto Profile { get; set; }
        [Required] public int AddressId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Потрібно хоча б один товар у замовленні")]
        public List<OrderItemDto> Items { get; set; }
    }
}
