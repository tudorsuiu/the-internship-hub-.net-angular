namespace TheInternshipHub.Server.Domain.DTOs.JSON;

public record ProfessionalExperience
{
    public string Company { get; set; }
    public string Role { get; set; }
    public string Duration { get; set; }
    public string Responsibilities { get; set; }
}
