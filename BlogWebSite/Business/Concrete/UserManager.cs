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

        public void Add(UserDto userDto)
        {
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
        }

        public void Delete(string email)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
