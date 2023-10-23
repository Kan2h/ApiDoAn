using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendApp.DbContexts;
using BackendApp.Entities;
using BackendApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using BackendApp.Dtos.Users;
using BackendApp.Dtos.Common;
using BackendApp.Services.Implements;

namespace BackendApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService UserService)
        {
            _userService = UserService;
        }

        [HttpPost("create")]
        public IActionResult Create(UserDto input)
        {
            try
            {
                _userService.CreateUser(input);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }
        [HttpPost("login")]
        public IActionResult Login(RequestDto input)
        {
            try
            {
                return Ok(_userService.Login(input));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }
        [HttpPut("update")]
        public IActionResult Update(int id, UserDto input)
        {
            try
            {
                _userService.UpdateUser(id, input);
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
                return Ok(_userService.GetAll());
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
                return Ok(_userService.GetById(id));
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
                _userService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }
        [HttpGet("{id}/get-all-item")]
        public IActionResult GetAllItem(int id)
        {
            try
            {
                return Ok(_userService.GetAllItem(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }
        [HttpGet("{id}/get-all-favorite-item")]
        public IActionResult GetAllFavoriteItem(int id)
        {
            try
            {
                return Ok(_userService.GetAllFavorite(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }

        [HttpPost("{id}/search-item")]
        public IActionResult SearchItem(KeywordDto input, int id)
        {
            try
            {
                return Ok(_userService.SearchItem(input, id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
            }
        }
    }
}
