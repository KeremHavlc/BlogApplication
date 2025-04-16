using Business.Abstract;
using Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthsController(IAuthService authService)
        {
            _authService = authService;
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

            Response.Cookies.Append("authToken", result.AccessToken, new CookieOptions
            {
                HttpOnly = false, //javascriptle erişilebilir
                Secure = true, //Https varsa true , http de false 
                SameSite = SameSiteMode.None
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
                    HttpOnly = false,
                    Secure = true,
                    SameSite = SameSiteMode.None
                });
            }
            return Ok(new { message = "Çıkış Başarılı!" });
        }
    }
}
