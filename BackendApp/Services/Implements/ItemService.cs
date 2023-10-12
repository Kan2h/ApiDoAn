using BackendApp.DbContexts;
using BackendApp.Dtos;
using BackendApp.Dtos.Common;
using BackendApp.Dtos.Items;
using BackendApp.Entities;
using BackendApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace BackendApp.Services.Implements
{
    public class ItemService : IItemService
    {
        private readonly AppDbContext _dbContext;
        public ItemService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateItem(ItemDto input)
        {
            _dbContext.Items.Add(new Item
            {
                Name = input.Name,
                Price = input.Price,
                Description = input.Description,
                Category = input.Category,
                ImageUrl = input.ImageUrl,
                
            });
            _dbContext.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var item = _dbContext.Items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                throw new Exception("Không tìm thấy thông tin sản phẩm");
            }
            _dbContext.Items.Remove(item);
            _dbContext.SaveChanges();
        }

        public List<Item> GetAll()
        {
            var result = _dbContext.Items.OrderByDescending(i => i.Name).ToList();

            if (result.Count == 0)
            {
                throw new Exception("Không có thông tin");
            }
            return result;
            
        }

        public Item GetById(int id)
        {
            var item = _dbContext.Items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                throw new Exception("Không tìm thấy thông tin sản phẩm");
            }
            return new Item
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                Category = item.Category,
                Description = item.Description,
                ImageUrl = item.ImageUrl,
            };
        }

        public void UpdateItem(Item input)
        {
            var item = _dbContext.Items.FirstOrDefault(i => i.Id == input.Id);
            if (item == null)
            {
                throw new Exception("Không tìm thấy thông tin sản phẩm");
            }
            item.Name = input.Name;
            item.Price = input.Price;
            item.Description = input.Description;
            item.Category = input.Category;
            item.ImageUrl = input.ImageUrl;
            _dbContext.SaveChanges();
        }
    }
}
