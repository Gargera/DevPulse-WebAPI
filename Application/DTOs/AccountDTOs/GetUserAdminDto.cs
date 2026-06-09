namespace Application.DTOs.AccountDTOs
{
    public class GetUserAdminDto
    {
        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public IList<string> Roles { get; set; } = null!;
    }
}