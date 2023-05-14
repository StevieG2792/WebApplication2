using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models.Orders;

namespace WebApplication2.ProductRepository
{
    public interface IOrderRepository
    {
        IActionResult InsertOrder(CreateOrderDto orderDto);
        IActionResult UpdateOrderAddress(string address, int orderId);
        IActionResult UpdateOrder(int orderId, UpdateOrderDto orderDto);
        IActionResult CancelOrder(int orderId);
        IActionResult SubmitOrder(int orderId);
        IActionResult GetOrderByID(int orderId);
        IEnumerable<RetrieveOrderDto> GetOrderByPage (int page);
        void Save();
    }
}
