namespace api_orders.src.dto;

public class OrderOutputDto
{
    public string Order { get; set; }
    public decimal Quantity { get; set; }
    public string ProductCode { get; set; }
    public string ProductDescription { get; set; }
    public string Image { get; set; }
    public decimal CycleTime { get; set; }

    public List<MaterialOutputDto> Materials { get; set; } = new();
}
