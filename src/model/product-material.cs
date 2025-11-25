namespace api_orders.src.model
{
    public class ProductMaterial
    {
        public string ProductCode { get; set; }
        public string MaterialCode { get; set; }
        public Product Product { get; set; }
        public Material Material { get; set; }
    }
}
