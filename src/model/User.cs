namespace api_orders.src.model;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public DateTime? InitialDate { get; set; } 
    public DateTime? EndDate { get; set; } 
}
