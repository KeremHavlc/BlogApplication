using Business.Abstract;
using Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
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
        public IActionResult Update(Guid id , UserDto userDto)
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
    }
}
