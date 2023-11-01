using BackendApp.Dtos.Carts;
using BackendApp.Dtos.Favorites;
using BackendApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BackendApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService CartService)
        {
            _cartService = CartService;
        }

        [HttpPost("add-to-cart")]
        public IActionResult Create(CartDto input)
        {
            try
            {
                _cartService.AddToCart(input);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }

        [HttpPut("update")]
        public IActionResult Update(UpdateCartDto input)
        {
            try
            {
                _cartService.UpdateCart(input);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DeleteCartDto input)
        {
            try
            {
                _cartService.DeleteCart(input);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }

        [HttpGet("get-all-item")]
        public IActionResult GetAllItem(int id)
        {
            try
            {
                return Ok(_cartService.GetAllItem(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }

        [HttpPost("order")]
        public IActionResult Order(int id)
        {
            try
            {
                _cartService.SubmitCart(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }
    }
}
