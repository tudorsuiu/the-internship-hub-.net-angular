namespace TheInternshipHub.Server.Domain.DTOs.JSON;

public record Education
{
    public string Institution { get; set; }
    public string Degree { get; set; }
    public string Graduated { get; set; }
}
