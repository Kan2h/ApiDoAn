using BackendApp.Entities;

namespace BackendApp.Dtos.Orders
{
    public class OrderDetailDto
    {
        public string OrderId { get; set; }
        public User User { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public decimal Totals { get; set; }
    }
}
