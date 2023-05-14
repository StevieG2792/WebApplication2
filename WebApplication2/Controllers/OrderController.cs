using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using WebApplication2.Models;
using WebApplication2.Models.Orders;
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
            return order;
        }

        [HttpGet("GetOrderByPage")]
        public IActionResult GetOrderByPage(int page)
        {
            var order = _orderRepository.GetOrderByPage(page);
            return new OkObjectResult(order);
        }

        [HttpPost("CreateOrder")]
        public IActionResult Create([FromBody] CreateOrderDto orderDto)
        {
            using (var scope = new TransactionScope())
            {                         
                var result = _orderRepository.InsertOrder(orderDto);
                scope.Complete();
                return result;
            }

        }

        [HttpPut("UpdateAddress")]
        public IActionResult UpdateAddress(string address, int orderId)
        {
            if (address != null)
            {
                using (var scope = new TransactionScope())
                {
                    var result = _orderRepository.UpdateOrderAddress(address, orderId);
                    scope.Complete();
                    return result;
                }
            }
            return new NoContentResult();
        }

        [HttpPut("UpdateOrder")]
        public IActionResult UpdateOrderItems(int orderId, UpdateOrderDto orderDto)
        {
            if (orderDto != null)
            {
                using (var scope = new TransactionScope())
                {
                  var result =  _orderRepository.UpdateOrder(orderId, orderDto);
                    scope.Complete();
                    return result;
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
                    var result = _orderRepository.CancelOrder(orderId);
                    scope.Complete();
                    return result;
                }
            }
            return new NoContentResult();
        }

        [HttpPut("SubmitOrder")]
        public IActionResult SubmitOrder(int orderId)
        {
            if (orderId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var result = _orderRepository.SubmitOrder(orderId);
                    scope.Complete();
                    return result;
                }
            }
            return new NoContentResult();
        }

    }
}
