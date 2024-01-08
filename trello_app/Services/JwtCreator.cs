using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using trello_app.Models;

namespace trello_app.Services
{
    public class JwtCreator
    {
        public static JwtSecurityToken CreateJwt(string email, string id, string name)
        {
            var claims = new List<Claim> 
            { 
                new Claim("email", email),
                new Claim("id", id),
                new Claim("name", name)
            };
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return jwt;
        }
    }
}
