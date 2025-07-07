namespace KBDTypeServer.Application.DTOs.ProductDtos;

public class ProductUniversalDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProductType { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string Manufacturer { get; set; }
    public Dictionary<string, object?> Characteristics { get; set; } = new();
}