using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApplication2.Models.Orders
{
    public class RetrieveOrderDto
    {
        public class Detail : DetailBase
        {
            public string Product { get; set; }
            public decimal Price { get; set; }
        }
        public string Customer { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public IEnumerable<Detail> Details { get; set; }
    }
}
