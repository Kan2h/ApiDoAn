using BackendApp.Dtos.Common;
using BackendApp.Dtos.Items;
using BackendApp.Dtos.Users;
using BackendApp.Entities;

namespace BackendApp.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetAll();
        void CreateUser(CreateUserDto input);
        void UpdateUser(int id, UpdateUserDto input);
        User GetById(int id);
        void Delete(int id);
        List<FavoriteItemDto> GetAllFavorite(int id);
        List<FavoriteItemDto> GetAllItem(int id);
        List<FavoriteItemDto> SearchItem(KeywordDto input, int id);
        User Login(RequestDto input);
    }
}
