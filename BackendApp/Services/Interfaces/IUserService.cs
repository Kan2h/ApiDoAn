using BackendApp.Dtos.Items;
using BackendApp.Dtos.Users;

namespace BackendApp.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetAll();
        void CreateUser(UserDto input);
        void UpdateUser(int id, UserDto input);
        UserDto GetById(int id);
        void Delete(int id);
        List<FavoriteItemDto> GetAllFavorite(int id);
        List<FavoriteItemDto> GetAllItem(int id);

        UserDto Login(RequestDto input);
    }
}
