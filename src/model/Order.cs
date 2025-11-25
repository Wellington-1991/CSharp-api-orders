namespace api_orders.src.model;

public class Order
{
    public int Id { get; set; }

    public string OrderCode { get; set; }
    public decimal Quantity { get; set; }

    public string ProductCode { get; set; }

    public Product Product { get; set; }
}
