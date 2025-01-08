namespace TheInternshipHub.Server.Domain.DTOs.JSON;

public record Candidate
{
    public PersonalInformation PersonalInformation { get; set; }
    public List<Education> Education { get; set; }
    public List<ProfessionalExperience> ProfessionalExperience { get; set; }
    public List<string> TechnicalSkills { get; set; }
    public List<string> Certifications { get; set; }
    public List<Project> Projects { get; set; }
    public List<Language> Languages { get; set; }
    public AdditionalInformation AdditionalInformation { get; set; }
}
