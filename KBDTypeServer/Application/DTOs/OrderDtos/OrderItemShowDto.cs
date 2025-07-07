namespace KBDTypeServer.Application.DTOs.OrderDtos
{
    public class OrderItemShowDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}