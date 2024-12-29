using TheInternshipHub.Server.Domain.Entities;

namespace TheInternshipHub.Server.Domain.DTOs
{
    public record InternshipDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public COMPANY Company { get; set; }

        public UserDTO Recruiter { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int PositionsAvailable { get; set; }

        public int Compensation { get; set; }

        public bool IsDeleted { get; set; }
    }
}
