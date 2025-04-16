using Core.Dtos;
using Core.Utilities.Security.Jwt;

namespace Business.Abstract
{
    public interface IAuthService
    {
        (bool success, string message) Register(RegisterDto registerDto);
        Token Login (LoginDto loginDto);
    }
}
