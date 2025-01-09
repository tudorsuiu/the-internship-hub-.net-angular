using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheInternshipHub.Server.Domain.DTOs.JSON;
using TheInternshipHub.Server.Domain.Interfaces;
using TheInternshipHub.Server.Domain.SmartService.Domain;

namespace TheInternshipHub.Server.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OpenAIController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IPdfToTextService _pdfToTextService;
    private readonly IAzureOpenAIService _azureOpenAIService;

    public OpenAIController(ApplicationDbContext context, IPdfToTextService pdfToTextService, IAzureOpenAIService azureOpenAIService)
    {
        _context = context;
        _pdfToTextService = pdfToTextService;
        _azureOpenAIService = azureOpenAIService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<string>> GetCandidateProfileByApplicationId(Guid id)
    {
        var userId = new Guid(User.FindFirstValue("id"));
        var userRole = User.FindFirstValue(ClaimTypes.Role);

        if (userRole != "Recruiter")
        {
            return Unauthorized();
        }

        var cvFilePath = _context.Applications.FirstOrDefault(a => a.AP_ID == id).AP_CV_FILE_PATH;

        if (cvFilePath == null)
        {
            return NotFound();
        }

        var cvToText = await _pdfToTextService.ExtractTextFromPdfAsync(cvFilePath);
        var candidateProfile = await _azureOpenAIService.GetOpenAIResponse(cvToText);

        return Ok(candidateProfile);
    }
}