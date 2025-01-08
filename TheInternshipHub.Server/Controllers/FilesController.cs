using Microsoft.AspNetCore.Mvc;
using TheInternshipHub.Server.Domain.Interfaces;

namespace TheInternshipHub.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IAzureBlobStorageService _azureBlobStorageService;

        const long maxFileSize = 3 * 1024 * 1024;

        public FilesController(IAzureBlobStorageService azureBlobStorageService)
        {
            _azureBlobStorageService = azureBlobStorageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is not provided or is empty.");
            }

            if (file.Length > maxFileSize)
            {
                return BadRequest("File is not provided or is empty.");
            }    

            var extension = Path.GetExtension(file.FileName).ToLower();
            if (extension != ".pdf")
            {
                return BadRequest("Only PDF files are allowed.");
            }

            var fileData = new byte[file.Length];
            using (var stream = file.OpenReadStream())
            {
                await stream.ReadAsync(fileData, 0, (int)file.Length);
            }

            var fileName = $"{Guid.NewGuid()}_resume{extension}";
            var fileUrl = await _azureBlobStorageService.UploadAsync(fileData, fileName);

            return Ok(new { url = fileUrl });
        }
    }
}
