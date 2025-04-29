using Business.Abstract;
using Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public AdminController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }
        [HttpPost("loginadmin")]
        public IActionResult LoginAdmin([FromBody] LoginDto loginDto)
        {
            var result = _authService.Login(loginDto);

            if (result == null || string.IsNullOrEmpty(result.AccessToken))
            {
                return Unauthorized(new { message = "Kullanıcı Adı veya Şifre Hatalı!" });
            }
            var user = _userService.GetByUsername(loginDto.Username);
            if(user.RoleId != Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa5"))
            {
                return Unauthorized(new { message = "Sadece Admin kullanıcı girişi yapabilir!" });
            }
            Response.Cookies.Append("authToken", result.AccessToken, new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            return Ok(new { message = "Admin Girişi Başarılı!" });
        }
    }
}
