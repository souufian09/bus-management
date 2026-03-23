using AuthService.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenererToken(Utilisateur user)
        {
            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),

                new Claim("email", user.Email),

                new Claim("role", user.Role),

                new Claim("nom", user.Nom)
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Secret"]!
                )
            );
            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );
            var token = new JwtSecurityToken(

                claims: claims,

                expires: DateTime.Now.AddHours(24),

                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
