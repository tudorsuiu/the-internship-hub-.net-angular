namespace TheInternshipHub.Server.Domain.Interfaces;

public interface IAzureOpenAIService
{
    Task<string> GetOpenAIResponse(string prompt);
}
