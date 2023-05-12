namespace WebApplication2.Models.Orders
{
    public class UpdateOrderDto
    {
        public class Detail
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
        public IEnumerable<Detail> Details { get; set; }
    }
}
