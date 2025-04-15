using Core.Dtos;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        void Add(UserDto userDto);
        void Delete(string email);
        void Update(Guid id, User user);
        User GetByEmail(string email);
        List<User> GetAll();

    }
}
