using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using WebApplication2.Models;
using WebApplication2.ProductRepository;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("GetOrderById")]
        public IActionResult GetOrderById(int id)
        {
            var order = _orderRepository.GetOrderByID(id);
            return new OkObjectResult(order);
        }

        [HttpGet("GetOrderByPage")]
        public IActionResult GetOrderByPage(int page)
        {
            var order = _orderRepository.GetOrderByPage(page);
            return new OkObjectResult(order);
        }

        [HttpPost("CreateOrder")]
        public ActionResult Create([FromBody] OrderDto orderDto)
        {
            using (var scope = new TransactionScope())
            {
                var order = new Order()
                {
                    Customer = orderDto.Customer,
                    Address = orderDto.Address,
                    Status = "Active",
                    OrderDetails = (from item in orderDto.Details
                                    select new OrderDetail()
                                    { ProductId = item.ProductId, Quantity = item.Quantity }).ToList()
                };
            
            _orderRepository.InsertOrder(order);
            scope.Complete();
                return new OkObjectResult("Order Saved, ID: "+ order.Id);
               // return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
            }

        }

        [HttpPut("UpdateAddress")]
        public IActionResult UpdateAddress(string address, int orderId)
        {
            if (address != null)
            {
                using (var scope = new TransactionScope())
                {
                    _orderRepository.UpdateOrderAddress(address, orderId);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpPut("UpdateOrder")]
        public IActionResult UpdateOrder(int orderId, OrderDto orderDto)
        {
            if (orderDto != null)
            {
                using (var scope = new TransactionScope())
                {
                    _orderRepository.UpdateOrder(orderId, orderDto);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpPut("CancelOrder")]
        public IActionResult CancelOrder(int orderId)
        {
            if (orderId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    _orderRepository.CancelOrder(orderId);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }
       
    }
}
