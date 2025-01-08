namespace TheInternshipHub.Server.Domain.DTOs.JSON;

public record PersonalInformation
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Contact { get; set; }
}
