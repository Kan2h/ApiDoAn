using BackendApp.Dtos.Favorites;
using BackendApp.Dtos.Items;
using BackendApp.Entities;
using BackendApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BackendApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService FavoriteService)
        {
            _favoriteService = FavoriteService;
        }

        [HttpPost("create")]
        public IActionResult Create(FavoriteDto input)
        {
            try
            {
                _favoriteService.AddFavorite(input);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }

        [HttpPut("update")]
        public IActionResult Update(FavoriteDto input)
        {
            try
            {
                _favoriteService.UpdateFavorite(input);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }

        [HttpDelete("delete")]
        public IActionResult Delete([Range(1, int.MaxValue, ErrorMessage = "Id phải lớn hơn 0")] int id)
        {
            try
            {
                _favoriteService.DeleteFavorite(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }

        [HttpGet("get-all")]
        public IActionResult GetAll(int id)
        {
            try
            {
                return Ok(_favoriteService.GetAllFavorite(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }
    }
}
