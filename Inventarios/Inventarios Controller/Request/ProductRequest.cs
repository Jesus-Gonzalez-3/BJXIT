namespace Inventarios_Controller.Request
{
    public class ProductRequest
    {
        public int ProductKey { get; set; }
        public string ProductName { get; set; }
        public int ProductCount { get; set; }
        public float ProductPrice { get; set; }
    }

    public class UpdateProductRequest
    {
        public int ProductId {  get; set; }
        public int ProductKey { get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }
    }
}
