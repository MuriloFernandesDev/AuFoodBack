using AuFood.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuFood.Auxiliary
{

    public class Token
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
    }

    public static class TokenService
    {

        public static string Secret = "52bbf9218b342601c7e2951db85b813a";

        public static Token GenerateToken(Login user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.Email.ToString()),
                }),
                Expires = DateTime.UtcNow.AddSeconds(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var accessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            var refreshToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            return new Token
            {
                access_token = accessToken,
                refresh_token = refreshToken,
            };
        }
    }
}
