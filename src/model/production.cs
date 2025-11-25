using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api_orders.src.model; 

namespace api_orders.src.model;

public class Production
{
    public long Id { get; set; }

    public string Email { get; set; }

    public string OrderCodeFK { get; set; } 

    public DateTime Date { get; set; }

    public decimal? Quantity { get; set; }

    public string MaterialCode { get; set; }

    public decimal? CycleTime { get; set; }

    public Order Order { get; set; }

    public User User { get; set; }
}