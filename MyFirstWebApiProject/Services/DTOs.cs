// src/Services/DTOs/AuthDtos.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace KBDTypeServer.Services
{
    /// <summary>
    /// Модель для логіну
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    /// <summary>
    /// Модель для реєстрації користувача
    /// </summary>
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Якщо true, призначити роль "Manager"
        /// </summary>
        public bool AssignAsManager { get; set; }
    }

    /// <summary>
    /// ДТО для збереження/отримання профілю користувача
    /// </summary>
    public class ProfileDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public AddressDto Address { get; set; }
    }

    /// <summary>
    /// ДТО для адреси доставки
    /// </summary>
    public class AddressDto
    {
        /// <summary>Область</summary>
        [MaxLength(100)]
        public string Region { get; set; }

        /// <summary>Місто</summary>
        [MaxLength(100)]
        public string City { get; set; }

        /// <summary>Вулиця</summary>
        [MaxLength(150)]
        public string Street { get; set; }

        /// <summary>Номер квартири/будинку</summary>
        [MaxLength(20)]
        public string Apartment { get; set; }

        /// <summary>Поштовий індекс</summary>
        [MaxLength(20)]
        public string PostalCode { get; set; }
    }
    public class CreateOrderDto
    {
        [Required] public ProfileDto Profile { get; set; }
        [Required] public int AddressId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Потрібно хоча б один товар у замовленні")]
        public List<OrderItemDto> Items { get; set; }
    }

    /// <summary>
    /// Елемент замовлення в DTO для створення замовлення
    /// </summary>
    public class OrderItemDto
    {
        /// <summary>Id товару</summary>
        public int ProductId { get; set; }

        /// <summary>Кількість</summary>
        public int Quantity { get; set; }

        // за потреби можна додати Price, Name тощо
    }

}
