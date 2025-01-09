namespace TheInternshipHub.Server.Domain.DTOs
{
    public record ApplicationDTO
    {
        public Guid Id { get; set; }

        public Guid InternshipId { get; set; }

        public InternshipDTO Internship { get; set; }

        public Guid StudentId { get; set; }

        public UserDTO Student { get; set; }

        public DateTime AppliedDate { get; set; }

        public string Status { get; set; }

        public string CVFilePath { get; set; }

        public string UniversityDocsFilePath { get; set; }

        public bool IsDeleted { get; set; }
    }
}
