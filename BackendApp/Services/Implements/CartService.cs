using BackendApp.DbContexts;
using BackendApp.Dtos.Carts;
using BackendApp.Dtos.Common;
using BackendApp.Dtos.Favorites;
using BackendApp.Dtos.Items;
using BackendApp.Entities;
using BackendApp.Services.Interfaces;

namespace BackendApp.Services.Implements
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _dbContext;
        public CartService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddToCart(CartDto input)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == input.UserId);
            if (user == null)
            {
                throw new Exception("Không tìm thấy thông tin người dùng");
            }
            
            var item = _dbContext.Items.FirstOrDefault(i => i.Id == input.ItemId); 
            if (item == null)
            {
                throw new Exception("Không tìm thấy thông tin sản phẩm");
            }
            
            var cartItem = _dbContext.Carts.FirstOrDefault(c => (c.ItemId == input.ItemId && c.UserId == input.UserId));
            if (cartItem != null)
            {
                cartItem.Quantity = cartItem.Quantity + input.Count;
            }
            else
            {
                _dbContext.Carts.Add(new Cart
                {
                    ItemId = input.ItemId,
                    UserId = input.UserId,
                    Quantity = input.Count
                });
            }
            _dbContext.SaveChanges();
        }
        public void UpdateCart(UpdateCartDto input)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == input.UserId);
            if (user == null)
            {
                throw new Exception("Không tìm thấy thông tin người dùng");
            }

            var item = _dbContext.Items.FirstOrDefault(i => i.Id == input.ItemId);
            if (item == null)
            {
                throw new Exception("Không tìm thấy thông tin sản phẩm");
            }

            var cartItem = _dbContext.Carts.FirstOrDefault(c => (c.ItemId == input.ItemId && c.UserId == input.UserId));
            if (cartItem == null)
            {
                throw new Exception("Không tìm thấy thông tin giỏ hàng");
            }
            cartItem.Quantity = input.Quantity;
            _dbContext.SaveChanges();
        }

        public void DeleteCart(DeleteCartDto input)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == input.UserId);
            if (user == null)
            {
                throw new Exception("Không tìm thấy thông tin người dùng");
            }

            var item = _dbContext.Items.FirstOrDefault(i => i.Id == input.ItemId);
            if (item == null)
            {
                throw new Exception("Không tìm thấy thông tin sản phẩm");
            }

            var cartItem = _dbContext.Carts.FirstOrDefault(c => (c.ItemId == input.ItemId && c.UserId == input.UserId));
            if (cartItem == null)
            {
                throw new Exception("Không tìm thấy thông tin giỏ hàng");
            }
            _dbContext.Carts.Remove(cartItem);
            _dbContext.SaveChanges();
        }

        public List<CartItemDto> GetAllItem(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new Exception("Không tìm thấy thông tin người dùng");
            }

            var result = from c in _dbContext.Carts
                         join i in _dbContext.Items on c.ItemId equals i.Id
                         join f in _dbContext.Favorites on i.Id equals f.ItemId into UserCart
                         from val in UserCart.DefaultIfEmpty()
                         where c.UserId == id
                         select new CartItemDto
                         {
                             Id = i.Id,
                             Category = i.Category,
                             Description = i.Description,
                             ImageUrl = i.ImageUrl,
                             Name = i.Name,
                             Price = i.Price,
                             IsFavorite = (val.IsFavorite != null) ? val.IsFavorite : false,
                             Quantity = c.Quantity
                         };
            return result.ToList();
        }

        public void SubmitCart(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new Exception("Không tìm thấy thông tin người dùng");
            };

            var query = _dbContext.Carts.Where(c => c.UserId == id).ToList();
            
            var str = RandStr();
            var findId = _dbContext.Orders.FirstOrDefault(o => o.OrderId == str);
            bool isFound = false;

            while (!isFound)
            {
                str = RandStr();
                findId = _dbContext.Orders.FirstOrDefault(o => o.OrderId == str);
                if (findId == null)
                {
                    isFound = true;
                }
            }

            _dbContext.Orders.Add(new Order
            {
                UserId = id,
                OrderId = str,
            });
            foreach (var item in query)
            {
                _dbContext.OrderDetails.Add(new OrderDetail
                {
                    OrderId = str,
                    ItemId = item.ItemId,
                    Quantity = item.Quantity
                });
                _dbContext.Carts.Remove(item);
            }

            _dbContext.SaveChanges();
        }

        private string RandStr()
        {
            Random rand = new Random();
            String key = "abcdefghijklmnopqrstuvwxyz0123456789";
            int size = 10;
            String str = "";

            for (int i = 0; i < size; i++)
            {
                int x = rand.Next(26);
                str = str + key[x];
            }
            return str;
        }
    }
}
