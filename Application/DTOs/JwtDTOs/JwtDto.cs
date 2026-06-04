namespace Application.DTOs.JwtDTOs
{
    public class JwtDto
    {
        public string UserId { get; set; } = null!;

        public string Email { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public IList<string> Roles { get; set; } = null!;
    }
}