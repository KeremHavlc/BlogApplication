﻿using Business.Abstract;
using Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add")]
        public IActionResult Add(UserDto userDto)
        {
            var result = _userService.Add(userDto);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
        [HttpDelete("delete/{email}")]
        public IActionResult Delete(string email)
        {
            var result = _userService.Delete(email);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
        [HttpPut("update")]
        public IActionResult Update(Guid id, UserDto userDto)
        {
            var result = _userService.Update(id, userDto);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            return Ok(result);
        }
        [HttpGet("getByUsername")]
        public IActionResult GetByUsername(string username)
        {
            var result = _userService.GetByUsername(username);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Kullanıcı Bulunamadı!");
        }
        [HttpGet("getByUsernameFront")]
        public IActionResult GetByUsernameFront(string username)
        {
            var result = _userService.GetByUsernameFront(username);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Kullanıcı Bulunamadı!");
        }
        [HttpGet("getById")]
        public IActionResult GetById(Guid id)
        {
            var result = _userService.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Kullanıcı Bulunamadı!");
        }
        [HttpGet("getByIdFront")]
        public IActionResult GetByIdFront(Guid id)
        {
            var result = _userService.GetByIdFront(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Kullanıcı Bulunamadı!");
        }
    }
}
