using System.ComponentModel.DataAnnotations;

namespace KBDTypeServer.Application.DTOs.OrderDtos
{
    public class OrderCreateDto
    {
        // Дані користувача
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        // Адреса доставки
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

        // Додатково
        public string? Comment { get; set; }

        // Список товарів у замовленні
        [Required]
        [MinLength(1, ErrorMessage = "Order must contain at least one item.")]
        public List<OrderItemCreateDto> Items { get; set; }
    }
}
