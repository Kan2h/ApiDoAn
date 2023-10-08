using BackendApp.DbContexts;
using BackendApp.Dtos;
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
            /*var results = new List<Item>();
            foreach (var item in _dbContext.Items)
            {
                results.Add(new Item
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Description = item.Description,
                    ImageUrl = item.ImageUrl,
                });
            }
            return results;*/

            var results = _dbContext.Items.OrderByDescending(i => i.Name).ToList();
            if (results.Count == 0) 
            {
                throw new Exception("Không có thông tin");
            }
            return results;
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
                Description = item.Description,
                ImageUrl = item.ImageUrl,
            };
        }

        public List<Item> SearchItem(KeywordDto input)
        {
            var results = new List<Item>();
            var query = _dbContext.Items.Where(i => (i.Name.Contains(input.Keyword)))
                                        .OrderBy(i => i.Price)
                                        .ThenByDescending(i => i.Id).ToList();
            if(query.Count == 0)
            {
                throw new Exception("Không tìm thấy thông tin sản phẩm");
            }

            foreach (var item in query)
            {
                results.Add(new Item
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Description = item.Description,
                    ImageUrl = item.ImageUrl,
                });
            }
            return results;
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
            item.ImageUrl = input.ImageUrl;
            _dbContext.SaveChanges();
        }
    }
}
