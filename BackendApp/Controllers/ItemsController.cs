using BackendApp.Dtos;
using BackendApp.Entities;
using BackendApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BackendApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService ItemService)
        {
            _itemService = ItemService;
        }

        [HttpPost("create")]
        public IActionResult Create(ItemDto input)
        {
            try
            {
                _itemService.CreateItem(input);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }
        
        [HttpPut("update")]
        public IActionResult Update(Item input)
        {
            try
            {
                _itemService.UpdateItem(input);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_itemService.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }
        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById([Range(1, int.MaxValue, ErrorMessage = "Id phải lớn hơn 0")] int id)
        {
            try
            {
                return Ok(_itemService.GetById(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }
        [HttpDelete("delete")]
        public IActionResult DeleteUser([Range(1, int.MaxValue, ErrorMessage = "Id phải lớn hơn 0")] int id)
        {
            try
            {
                _itemService.DeleteItem(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }
        [HttpPost("search")]
        public IActionResult SearchItem(KeywordDto input)
        {
            try
            {
                return Ok(_itemService.SearchItem(input));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }

    }
}
