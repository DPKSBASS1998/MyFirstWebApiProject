// OrderDto.cs
using System;
using System.Collections.Generic;

namespace KBDTypeServer.Application.DTOs.Order
{
    /// <summary>
    /// DTO для виводу інформації по замовленню
    /// </summary>
    public class OrderDto
    {
        public int OrderId { get; set; }           // Id замовлення
        public string Username { get; set; } = null!; // Ім’я користувача
        public AddressDto Address { get; set; } = null!; // Адреса доставки
        public DateTime CreatedAt { get; set; }    // Дата створення
        public List<OrderItemDetailDto> Items { get; set; } = new(); // Деталі товарів
    }
}
