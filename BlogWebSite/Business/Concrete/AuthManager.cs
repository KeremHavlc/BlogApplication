using Business.Abstract;
using Core.Constants;
using Core.Dtos;
using Core.Utilities.HashingHelper;
using Core.Utilities.Security.Jwt;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {

        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;
        public AuthManager(IUserService userService , ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }
        public Token Login(LoginDto loginDto)
        {
            var user = _userService.GetByUsername(loginDto.Username);
            if(user == null)
            {
                return null;
            }
            var result = HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);
            if (!result)
            {
                return null;
            }
            var token = _tokenHandler.CreateToken(user.Id, user.Username, user.RoleId.ToString());
            return token;
        }

        public (bool success, string message) Register(RegisterDto registerDto)
        {
            if (string.IsNullOrWhiteSpace(registerDto.Email))
            {
                return (false, "Email Adresi Boş Olamaz!");
            }
            if (string.IsNullOrWhiteSpace(registerDto.Username))
            {
                return (false, "Kullanıcı Adı Boş Olamaz!");
            }
            if (string.IsNullOrWhiteSpace(registerDto.password))
            {
                return (false, "Şifre Boş Olamaz!");
            }

            Guid defaultRoleId = RoleGuids.User;
            Guid assignedRoleId = registerDto.RoleId ?? defaultRoleId;
            var userDto = new UserDto
            {
                Email = registerDto.Email,
                Username = registerDto.Username,
                Password = registerDto.password,
                RoleId = assignedRoleId
            };
            try
            {
                _userService.Add(userDto);
                return (true, "Kayıt İşlemi Başarılı!");
            }
            catch (Exception)
            {

                return (false, "Kayıt işleminde bir hata oluştu!");
            }
        }
    }
}
