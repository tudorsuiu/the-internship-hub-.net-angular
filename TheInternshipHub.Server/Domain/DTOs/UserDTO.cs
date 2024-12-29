using TheInternshipHub.Server.Domain.Entities;

namespace TheInternshipHub.Server.Domain.DTOs
{
    public record UserDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public COMPANY Company { get; set; }

        public string Role { get; set; }

        public string City { get; set; }

        public bool IsDeleted { get; set; }
    }
}
