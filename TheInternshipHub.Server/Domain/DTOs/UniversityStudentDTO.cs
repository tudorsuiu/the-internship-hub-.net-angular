namespace TheInternshipHub.Server.Domain.DTOs
{
    public record UniversityStudentDTO
    {
        public Guid StudentId { get; set; }

        public string StudentFirstName { get; set; }

        public string StudentLastName { get; set; }

        public string StudentEmail { get; set; }

        public string StudentPhoneNumber { get; set; }

        public string University { get; set; }

        public Guid ApplicationId { get; set; }

        public Guid InternshipId { get; set; }

        public string ApplicationStatus { get; set; }

        public string ApplicationCVFilePath {  get; set; }
    }
}
