using BackendApp.Dtos;
using BackendApp.Dtos.Common;
using BackendApp.Dtos.Items;
using BackendApp.Entities;

namespace BackendApp.Services.Interfaces
{
    public interface IItemService
    {
        List<Item> GetAll();
        void CreateItem(CreateItemDto input);
        void UpdateItem(Item input);
        Item GetById(int id);
        void DeleteItem(int id);
    }
}
