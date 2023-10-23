using BackendApp.DbContexts;
using BackendApp.Dtos.Common;
using BackendApp.Dtos.Items;
using BackendApp.Dtos.Users;
using BackendApp.Entities;
using BackendApp.Services.Interfaces;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BackendApp.Services.Implements
{
    public class UserService:IUserService
    {
        private readonly AppDbContext _dbContext;
        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateUser(UserDto input)
        {
            _dbContext.Users.Add(new User
            {
                Name = input.Name,
                Email = input.Email,
                Password = input.Password,
            });
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new Exception("Không tìm thấy thông tin người dùng");
            }
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public List<User> GetAll()
        {
            var results = new List<User>();
            foreach (var user in _dbContext.Users)
            {
                results.Add(new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                });
            }
            return results;
        }

        public UserDto GetById(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(e => e.Id == id);
            if (user == null)
            {
                throw new Exception("Không tìm thấy thông tin người dùng");
            }
            return new UserDto
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };
        }

        public void UpdateUser(int id, UserDto input)
        {
            var user = _dbContext.Users.FirstOrDefault(o => o.Id == id);
            if (user == null)
            {
                throw new Exception("Không tìm thấy thông tin người dùng");
            }
            user.Name = input.Name;
            user.Email = input.Email;
            user.Password = input.Password;
            _dbContext.SaveChanges();
        }

        public User Login(RequestDto input)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == input.Email && u.Password == input.Password);
            if (user == null)
            {
                throw new Exception("Không tìm thấy thông tin người dùng");
            }
            return new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };
        }
        public List<FavoriteItemDto> GetAllFavorite(int id)
        {
            var query = from f in _dbContext.Favorites
                        join i in _dbContext.Items on f.ItemId equals i.Id
                        where f.UserId == id
                        where f.IsFavorite == true
                        select new FavoriteItemDto
                        {
                            Id = i.Id,
                            Name = i.Name,
                            Category = i.Category,
                            Description = i.Description,
                            Price = i.Price,
                            ImageUrl = i.ImageUrl,
                            IsFavorite = f.IsFavorite
                        };
            var result = query.ToList();
            return result;

        }
        public List<FavoriteItemDto> GetAllItem(int id)
        {
            var results = new List<FavoriteItemDto>();

            foreach (var i in _dbContext.Items)
            {
                var query = _dbContext.Favorites.FirstOrDefault(f => f.UserId == id && f.ItemId == i.Id);
                results.Add(new FavoriteItemDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Category= i.Category,
                    Description = i.Description,
                    Price = i.Price,
                    ImageUrl = i.ImageUrl,
                    IsFavorite = (query != null) ? query.IsFavorite : false,
                });
            }
            return results;

        }
        
        public List<FavoriteItemDto> SearchItem(KeywordDto input, int id)
        {
            var results = new List<FavoriteItemDto>();

            foreach (var i in _dbContext.Items)
            {
                if ( i.Name.Contains(input.Keyword))
                {
                    var query = _dbContext.Favorites.FirstOrDefault(f => f.UserId == id && f.ItemId == i.Id);
                    results.Add(new FavoriteItemDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Category = i.Category,
                        Description = i.Description,
                        Price = i.Price,
                        ImageUrl = i.ImageUrl,
                        IsFavorite = (query != null) ? query.IsFavorite : false,
                    });
                }
            }
            return results;

            /*var query = from i in _dbContext.Items
                        join f in _dbContext.Favorites on i.Id equals f.ItemId into Details
                        from val in Details.DefaultIfEmpty()
                        where i.Name.Contains(input.Keyword)
                        select new FavoriteItemDto
                        {
                            Id = i.Id,
                            Name = i.Name,
                            Category = i.Category,
                            Description = i.Description,
                            Price = i.Price,
                            ImageUrl = i.ImageUrl,
                            IsFavorite = (val.IsFavorite != null) ? val.IsFavorite : false
                        };
            var result = query.ToList();
            return result.ToList();*/
        }
    }
}
