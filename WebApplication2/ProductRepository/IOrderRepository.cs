using WebApplication2.Models;

namespace WebApplication2.ProductRepository
{
    public interface IOrderRepository
    {
        void InsertOrder(Order order);
        void UpdateOrderAddress(string address, int orderId);
        void UpdateOrder(int orderId, RetrieveOrderDto orderDto);
        void CancelOrder(int orderId);
        RetrieveOrderDto GetOrderByID(int orderId);
        IEnumerable<RetrieveOrderDto> GetOrderByPage (int orderId);
        void Save();
    }
}
