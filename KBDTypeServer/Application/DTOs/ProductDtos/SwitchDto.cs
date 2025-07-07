namespace KBDTypeServer.Application.DTOs.ProductDtos;

public class SwitchDto : ProductDto
{
    public int SwitchType { get; set; }
    public int OperatingForce { get; set; }
    public double TotalTravel { get; set; }
    public double PreTravel { get; set; }
    public double TactilePosition { get; set; }
    public int TactileForce { get; set; }
    public int PinCount { get; set; }
}