using BackendApp.Dtos;

namespace BackendApp.Services.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetAll();
        void CreateUser(UserDto input);
        void UpdateUser(int id, UserDto input);
        UserDto GetById(int id);
        void Delete(int id);
        UserDto Login(RequestDto input);
    }
}
