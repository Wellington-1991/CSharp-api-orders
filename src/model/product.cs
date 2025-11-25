namespace api_orders.src.model
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal CycleTime { get; set; }

        public ICollection<ProductMaterial> ProductMaterials { get; set; }
    }
}
