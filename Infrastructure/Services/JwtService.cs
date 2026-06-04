using Application.DTOs.JwtDTOs;
using Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(JwtDto jwtDto)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, jwtDto.UserId),
                new Claim(ClaimTypes.Name, jwtDto.UserName),
                new Claim(ClaimTypes.Email, jwtDto.Email!)
            };

            foreach (var role in jwtDto.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var secretKey = new SymmetricSecurityKey
            (
                Encoding.ASCII.GetBytes(_configuration["JWT:Key"]!)
            );

            var signingCredentials = new SigningCredentials(
                secretKey,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken
            (
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"]!)),
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}