namespace WebApplication2.Models.Orders
{
    public class CreateOrderDto
    {
        public string Customer { get; set; }
        public string Address { get; set; }
        public IEnumerable<DetailBase> Details { get; set; }
    }
}
