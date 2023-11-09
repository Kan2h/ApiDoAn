using BackendApp.DbContexts;
using BackendApp.Dtos.Favorites;
using BackendApp.Dtos.Items;
using BackendApp.Entities;
using BackendApp.Services.Interfaces;

namespace BackendApp.Services.Implements
{
    public class FavoriteService : IFavoriteService
    {
        private readonly AppDbContext _dbContext;
        public FavoriteService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddFavorite(FavoriteDto input)
        {
            var query = _dbContext.Favorites.FirstOrDefault(f => f.UserId == input.UserId && f.ItemId == input.ItemId);
            if (query == null) {
                _dbContext.Favorites.Add(new Favorite
                {
                    UserId = input.UserId,
                    ItemId = input.ItemId,
                    IsFavorite = true,
                });
            } else {
                query.IsFavorite = !query.IsFavorite;
            }
            
            _dbContext.SaveChanges();
        }

        public void DeleteFavorite(int id)
        {
            var query = _dbContext.Favorites.Where(i => i.UserId == id);
            if (query == null)
            {
                throw new Exception("Không tìm thấy thông tin người dùng");
            }
            foreach (var item in query)
            {
                _dbContext.Favorites.Remove(item);

            }
            _dbContext.SaveChanges();
        }

        public void UpdateFavorite(FavoriteDto input)
        {
            var item = _dbContext.Favorites.FirstOrDefault(i => (i.UserId == input.UserId && i.ItemId == input.ItemId));
            if (item == null)
            {
                throw new Exception("Không tìm thấy thông tin sản phẩm");
            }
            item.UserId = input.UserId;
            item.ItemId = input.ItemId;
            item.IsFavorite = !item.IsFavorite;
            _dbContext.SaveChanges();
        }

        public List<FavoriteItemDto> GetAllFavorite(int id)
        {
            var query = from i in _dbContext.Items
                        join f in _dbContext.Favorites on i.Id equals f.ItemId
                        where f.UserId == id
                        select new FavoriteItemDto
                        {
                            Id = i.Id,
                            Name = i.Name,
                            Category = i.Category,
                            Description = i.Description,
                            ImageUrl = i.ImageUrl,
                            Price = i.Price,
                            IsFavorite = f.IsFavorite
                        };
            if (query == null)
            {
                throw new Exception("Không tìm thấy thông tin sản phẩm");
            }
            var result = query.ToList();
            return result;

        }
    }
}
