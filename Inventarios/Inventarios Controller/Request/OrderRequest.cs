namespace Inventarios_Controller.Request
{
    public class OrderRequest
    {
        public int Quantity { get; set; }
        public string CustomerName { get; set; }
        public int ProductId { get; set; }
        public int UserId {  get; set; }
    }
}
