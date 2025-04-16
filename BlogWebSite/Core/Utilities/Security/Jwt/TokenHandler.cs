
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    public class TokenHandler : ITokenHandler
    {
        IConfiguration Configuration;
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public Token CreateToken(Guid id, string username, string roleId)
        {
            Token token = new Token();

            //SecurityKey simetriği alınır.
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));

            //Şifrelenmiş securitykey
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //Token oluşturma işlemi
            token.Expiration = DateTime.Now.AddMinutes(60);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: token.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials,
                claims: setClaims(id,username,roleId)
            );

            //Token oluşturma işlemi
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            return token;
        }

        public IEnumerable<Claim> setClaims(Guid id, string username, string roleId)
        {
            var claims = new List<Claim>
           {
                new Claim("id", id.ToString()),
                new Claim("username", username),
                new Claim(ClaimTypes.Role, roleId),
           };
            return claims;
        }
    }
}
