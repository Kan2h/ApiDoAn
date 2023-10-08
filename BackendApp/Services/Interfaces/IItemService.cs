using BackendApp.Dtos;
using BackendApp.Entities;

namespace BackendApp.Services.Interfaces
{
    public interface IItemService
    {
        List<Item> GetAll();
        void CreateItem(ItemDto input);
        void UpdateItem(Item input);
        Item GetById(int id);
        void DeleteItem(int id);
        List<Item> SearchItem(KeywordDto input);
    }
}
