using Business.Abstract;
using Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthsController(IAuthService authService,IUserService userService)
        {
            _authService = authService;
            _userService = userService;

        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            var result = _authService.Register(registerDto);
            if (result.success)
            {
                return Ok(result.message);
            }
            return BadRequest(result.message);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var result = _authService.Login(loginDto);

            if (result == null || string.IsNullOrEmpty(result.AccessToken))
            {
                return Unauthorized(new {message = "Kullanıcı Adı veya Şifre Hatalı!" });
            }
            var user = _userService.GetByUsername(loginDto.Username);
            if (user.RoleId != Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"))
            {
                return Unauthorized(new { message = "Sadece  kullanıcı girişi yapabilir!" });
            }
            Response.Cookies.Append("authToken", result.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, 
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });
            return Ok(new { message = "Giriş Başarılı!" });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            if (Request.Cookies["authToken"] != null)
            {
                Response.Cookies.Append("authToken", "", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays(-1)
                });
            }
            return Ok(new { message = "Çıkış Başarılı!" });
        }
        [HttpGet("me")]
        public IActionResult Me()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(new { id = userId });
        }
    }
}
