namespace WebApplication2.Models.Orders
{
    public class CreateOrderDto
    {
        public class Detail
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
        public string Customer { get; set; }
        public string Address { get; set; }
        public IEnumerable<Detail> Details { get; set; }
    }
}
