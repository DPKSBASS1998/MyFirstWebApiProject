// OrderItemDetailDto.cs
namespace KBDTypeServer.Application.DTOs.Order
{
    /// <summary>
    /// Деталі елемента замовлення при виведенні
    /// </summary>
    public class OrderItemDetailDto
    {
        public int ProductId { get; set; }   // Id товару
        public string Name { get; set; } = null!; // Назва товару
        public int Quantity { get; set; }    // Кількість
        public decimal Price { get; set; }   // Ціна на момент замовлення
    }
}
