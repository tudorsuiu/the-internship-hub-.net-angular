namespace TheInternshipHub.Server.Domain.Interfaces;

public interface IPdfToTextService
{
    Task<string> ExtractTextFromPdfAsync(string pdfUrl);
}
