using BackendApp.Dtos.Carts;
using BackendApp.Dtos.Favorites;
using BackendApp.Dtos.Items;

namespace BackendApp.Services.Interfaces
{
    public interface ICartService
    {
        void AddToCart(CartDto input);
        void UpdateCart(UpdateCartDto input);
        void SubmitCart(int id);
        void DeleteCart(DeleteCartDto input);
        List<CartItemDto> GetAllItem(int id);
    }
}
