using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApplication2.Models
{
    public class OrderDto
    {
        public class Detail
        {
            public int ProductId { get; set; }
            public string Product { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }
        public string Customer { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public IEnumerable<Detail> Details { get; set; }
    }
}
