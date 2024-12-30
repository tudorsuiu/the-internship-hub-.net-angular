using TheInternshipHub.Server.Domain.Entities;

namespace TheInternshipHub.Server.Domain.DTOs
{
    public record UserRegisterDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string City { get; set; }

        public Guid CompanyId { get; set; }
    }
}
