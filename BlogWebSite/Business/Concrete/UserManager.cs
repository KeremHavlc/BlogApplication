using Business.Abstract;
using Core.Constants;
using Core.Dtos;
using Core.Utilities.HashingHelper;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public (bool success, string message) Add(UserDto userDto)
        {
            if(_userDal.Get(x=>x.Email == userDto.Email) != null)
            {
                return (false, "Bu Email Adresi Zaten Kayıtlı!");
            }
            if(_userDal.Get(x=>x.Username == userDto.Username) != null)
            {
                return (false, "Bu Kullanıcı Adi Zaten Kayıtlı!");
            }
            if(string.IsNullOrEmpty(userDto.Password) || userDto.Password.Length < 6)
            {
                return (false, "Geçerli bir Şifre Giriniz!");
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

            Guid defaultRoleId = RoleGuids.User;
            Guid assignedRoleId = userDto.RoleId ?? defaultRoleId;

            User user = new User
            {
                Id = Guid.NewGuid(),
                Email = userDto.Email,
                Username = userDto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = assignedRoleId
            };

            _userDal.Add(user);
            return (true, "Kullanıcı Ekleme İşlemi Başarılı!");
        }

        public (bool success, string message) Delete(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return (false, "Email Adresi Giriniz!");
            }
            var user = _userDal.Get(x => x.Email == email);
            if(user is null)
            {
                return (false, "Kullanıcı Bulunamadı!");
            }
            _userDal.Delete(user);
            return (true, "Kullanıcı Silme İşlemi Başarılı!");
        }

        public List<UserDto> GetAll()
        {
            var users = _userDal.GetAll();
            var listUser = users.Select(user => new UserDto
            {
                Email = user.Email,
                Username = user.Username,
                RoleId = user.RoleId
            }).ToList();
            return listUser;
        }

        public User GetByEmail(string email)
        {
            var user = _userDal.Get(x => x.Email == email);
            if(user is null)
            {
                throw new KeyNotFoundException("Kullanıcı Bulunamadı!");
            }
            return user;
        }

        public (bool success, string message) Update(Guid id, UserDto userDto)
        {
            Guid defaultRoleId = new Guid("00000000-0000-0000-0000-000000000002");
            Guid assignedRoleId = userDto.RoleId ?? defaultRoleId;

            var existingUser = _userDal.Get(x => x.Id == id);
            if(existingUser is null)
            {
                return (false, "Kullanıcı Bulunamadı!");
            }
            existingUser.Email = userDto.Email;
            existingUser.Username = userDto.Username;
            existingUser.RoleId = assignedRoleId;
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);
                existingUser.PasswordHash = passwordHash;
                existingUser.PasswordSalt = passwordSalt;   
            }
            _userDal.Update(existingUser);
            return (true, "Kullanıcı Güncelleme İşlemi Başarılı!");
           
        }
    }
}
