using WebApplication2.Models;

namespace WebApplication2.ProductRepository
{
    public interface IOrderRepository
    {
        void InsertOrder(Order order);
        void UpdateOrderAddress(string address, int orderId);
        void UpdateOrder(int orderId, OrderDto orderDto);
        void CancelOrder(int orderId);
        OrderDto GetOrderByID(int orderId);
        IEnumerable<OrderDto> GetOrderByPage (int orderId);
        void Save();
    }
}
