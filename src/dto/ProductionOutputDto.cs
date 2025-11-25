using System;

namespace api_orders.src.dto;

public class ProductionOutputDto
{
    public string Order { get; set; }
    public DateTime Date { get; set; }
    public decimal Quantity { get; set; }
    public string MaterialCode { get; set; }
    public decimal CycleTime { get; set; }
}
