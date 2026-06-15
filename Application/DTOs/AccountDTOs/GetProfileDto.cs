namespace Application.DTOs.AccountDTOs
{
    public class GetProfileDto
    {
        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}
