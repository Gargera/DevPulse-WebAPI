using Application.DTOs.JwtDTOs;

namespace Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(JwtDto jwtDto);
    }
}
