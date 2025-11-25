namespace api_orders.src.dto;

public class OrdersResponseDto
{
    public List<OrderOutputDto> Orders { get; set; } = new();
}
