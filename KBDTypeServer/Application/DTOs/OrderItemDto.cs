namespace KBDTypeServer.Application.DTOs
{
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
