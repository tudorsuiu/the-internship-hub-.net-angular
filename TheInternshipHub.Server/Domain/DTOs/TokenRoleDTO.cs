namespace TheInternshipHub.Server.Domain.DTOs
{
    public record TokenRoleDTO
    {
        public string Token { get; set; }

        public string Role { get; set; }
    }
}
