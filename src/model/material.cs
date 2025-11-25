namespace api_orders.src.model;

public class Material
{
    public int Id { get; set; }
    public string MaterialCode { get; set; }
    public string Description { get; set; }

    public ICollection<ProductMaterial> ProductMaterials { get; set; }
}
