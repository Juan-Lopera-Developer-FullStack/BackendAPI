using BackendAPI.Services.IServices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendAPI.Services
{
    public class BearerToken : IBearerToken
    {
        private readonly IConfiguration _config;

        public BearerToken(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateTokenJwt(string user, string password)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user),
                new Claim(ClaimTypes.SerialNumber, password)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }
    }
}
