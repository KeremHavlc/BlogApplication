using Core.Dtos;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        (bool success , string message) Add(UserDto userDto);
        (bool success , string message) Delete(string email);
        (bool success , string message) Update(Guid id, UserDto userDto);
        User GetByEmail(string email);
        User GetByUsername(string username);
        List<UserDto> GetAll();

    }
}
