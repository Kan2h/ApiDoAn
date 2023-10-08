using BackendApp.DbContexts;
using BackendApp.Dtos;
using BackendApp.Entities;
using BackendApp.Services.Interfaces;
using System;

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

        public List<UserDto> GetAll()
        {
            var results = new List<UserDto>();
            foreach (var user in _dbContext.Users)
            {
                results.Add(new UserDto
                {
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

        public UserDto Login(RequestDto input)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == input.Email && u.Password == input.Password);
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
    }
}
