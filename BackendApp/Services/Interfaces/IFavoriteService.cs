using BackendApp.Dtos.Favorites;
using BackendApp.Dtos.Items;
using BackendApp.Entities;

namespace BackendApp.Services.Interfaces
{
    public interface IFavoriteService
    {
        void AddFavorite(FavoriteDto input);
        void UpdateFavorite(FavoriteDto input);
        void DeleteFavorite(int id);
        List<FavoriteItemDto> GetAllFavorite(int id);
    }
}
