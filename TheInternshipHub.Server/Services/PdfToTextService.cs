using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using TheInternshipHub.Server.Domain.Interfaces;

namespace TheInternshipHub.Server.Services;

public class PdfToTextService : IPdfToTextService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PdfToTextService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> ExtractTextFromPdfAsync(string pdfUrl)
    {
        if (string.IsNullOrWhiteSpace(pdfUrl))
            throw new ArgumentException("The PDF URL cannot be null or empty.");

        try
        {
            byte[] pdfData = await DownloadPdfAsync(pdfUrl);

            return ExtractTextFromPdf(pdfData);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while extracting text from the PDF.", ex);
        }
    }

    private async Task<byte[]> DownloadPdfAsync(string pdfUrl)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync(pdfUrl);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to download the PDF from {pdfUrl}. Status code: {response.StatusCode}");

        return await response.Content.ReadAsByteArrayAsync();
    }

    private string ExtractTextFromPdf(byte[] pdfData)
    {
        using var pdfStream = new MemoryStream(pdfData);
        using var pdfReader = new PdfReader(pdfStream);
        using var pdfDocument = new PdfDocument(pdfReader);

        var textBuilder = new System.Text.StringBuilder();

        for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
        {
            var page = pdfDocument.GetPage(i);
            var text = PdfTextExtractor.GetTextFromPage(page);
            textBuilder.AppendLine(text);
        }

        return textBuilder.ToString();
    }
}
