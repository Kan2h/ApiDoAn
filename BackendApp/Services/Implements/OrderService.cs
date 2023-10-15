using BackendApp.DbContexts;
using BackendApp.Dtos.Orders;
using BackendApp.Services.Interfaces;

namespace BackendApp.Services.Implements
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _dbContext;
        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteOrderById(string orderId)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                throw new Exception("Không tìm thấy thông tin hóa đơn");
            }
            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();
        }

        public List<OrderDetailDto> GetAllOrder()
        {
            var result = new List<OrderDetailDto>();
            var query = _dbContext.Orders.OrderBy(o => o.Id).ToList();
            if (query.Count == 0)
            {
                throw new Exception("Không có hóa đơn nào");
            }
            foreach (var input in query)
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == input.UserId);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy thông tin người dùng");
                }
                var item = from o in _dbContext.OrderDetails
                           join i in _dbContext.Items on o.ItemId equals i.Id
                           where o.OrderId == input.OrderId
                           select new OrderItemDto
                           {
                               Id = i.Id,
                               Name = i.Name,
                               Category = i.Category,
                               Description = i.Description,
                               ImageUrl = i.ImageUrl,
                               Price = i.Price,
                               Quantity = o.Quantity,
                           };
                decimal total = 0;
                foreach (var j in item)
                {
                    total = total + j.Price * j.Quantity;
                }

                result.Add(new OrderDetailDto
                {
                    OrderId = input.OrderId,
                    User = user,
                    Items = item.ToList(),
                    Totals = total,
                });
            }

            return result;
        }

        public OrderDetailDto GetOrderById(string orderId)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                throw new Exception("Không tìm thấy thông tin hóa đơn");
            }

            var result = new OrderDetailDto();
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == order.UserId);
            if (user == null)
            {
                throw new Exception("Không tìm thấy thông tin người dùng");
            }
            var item = from o in _dbContext.OrderDetails
                       join i in _dbContext.Items on o.ItemId equals i.Id
                       where o.OrderId == order.OrderId
                       select new OrderItemDto
                       {
                           Id = i.Id,
                           Name = i.Name,
                           Category = i.Category,
                           Description = i.Description,
                           ImageUrl = i.ImageUrl,
                           Price = i.Price,
                           Quantity = o.Quantity,
                       };
            decimal total = 0;
            foreach (var j in item)
            {
                total = total + j.Price * j.Quantity;
            }

            result = new OrderDetailDto
            {
                OrderId = orderId,
                User = user,
                Items = item.ToList(),
                Totals = total,
            };

            return result;
        }

        public List<OrderDetailDto> GetOrderByUserId(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new Exception("Không tìm thấy thông tin người dùng");
            }

            var result = new List<OrderDetailDto>();
            var query = _dbContext.Orders.Where(o => o.UserId == id).ToList();

            if (query.Count == 0)
            {
                throw new Exception("Không có hóa đơn nào");
            }

            foreach (var input in query)
            {
                var item = from o in _dbContext.OrderDetails
                           join i in _dbContext.Items on o.ItemId equals i.Id
                           where o.OrderId == input.OrderId
                           select new OrderItemDto
                           {
                               Id = i.Id,
                               Name = i.Name,
                               Category = i.Category,
                               Description = i.Description,
                               ImageUrl = i.ImageUrl,
                               Price = i.Price,
                               Quantity = o.Quantity,
                           };

                decimal total = 0;
                foreach (var j in item)
                {
                    total = total + j.Price * j.Quantity;
                }

                result.Add(new OrderDetailDto
                {
                    OrderId = input.OrderId,
                    User = user,
                    Items = item.ToList(),
                    Totals = total,
                });
            }

            return result;
        }
    }
}
