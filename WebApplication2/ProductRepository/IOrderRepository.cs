using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models.Orders;

namespace WebApplication2.ProductRepository
{
    public interface IOrderRepository
    {
        IActionResult InsertOrder(CreateOrderDto orderDto);
        void UpdateOrderAddress(string address, int orderId);
        IActionResult UpdateOrder(int orderId, UpdateOrderDto orderDto);
        void CancelOrder(int orderId);
        void SubmitOrder(int orderId);
        RetrieveOrderDto GetOrderByID(int orderId);
        IEnumerable<RetrieveOrderDto> GetOrderByPage (int page);
        void Save();
    }
}
