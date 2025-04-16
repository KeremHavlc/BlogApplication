namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHandler
    {
        Token CreateToken(Guid id, string username, string roleId);
    }
}
