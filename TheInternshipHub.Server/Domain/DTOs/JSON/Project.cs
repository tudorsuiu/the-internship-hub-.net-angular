namespace TheInternshipHub.Server.Domain.DTOs.JSON;

public record Project
{
    public string ProjectName { get; set; }
    public string Description { get; set; }
    public List<string> TechnologiesUsed { get; set; }
}
