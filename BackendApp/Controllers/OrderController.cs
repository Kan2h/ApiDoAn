using BackendApp.Dtos.Favorites;
using BackendApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BackendApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService OrderService)
        {
            _orderService = OrderService;
        }

        [HttpDelete("delete")]
        public IActionResult Delete(string id)
        {
            try
            {
                _orderService.DeleteOrderById(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }

        [HttpGet("get-all-order")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_orderService.GetAllOrder());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }

        [HttpGet("get-order-by-id/{id}")]
        public IActionResult GetOrderById(string id)
        {
            try
            {
                return Ok(_orderService.GetOrderById(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }
        [HttpGet("get-order-by-user/{id}")]
        public IActionResult GetOrderByUser(int id)
        {
            try
            {
                return Ok(_orderService.GetOrderByUserId(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }
    }
}
