using BackendApp.Dtos.Orders;

namespace BackendApp.Services.Interfaces
{
    public interface IOrderService
    {
        List<OrderDetailDto> GetOrderByUserId(int id);
        void DeleteOrderById(string orderId);
        OrderDetailDto GetOrderById(string orderId);
        List<OrderDetailDto> GetAllOrder();
    }
}
