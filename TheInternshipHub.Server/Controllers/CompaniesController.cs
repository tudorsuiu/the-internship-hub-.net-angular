using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheInternshipHub.Server.Domain.DTOs;
using TheInternshipHub.Server.Domain.Entities;
using TheInternshipHub.Server.Domain.SmartService.Domain;

namespace TheInternshipHub.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<COMPANY>>> GetCompanies()
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole == "Student" || userRole == "University")
            {
                return _context.Companies
                    .Where(c => c.CO_NAME.Contains("Universitatea"))
                    .Order()
                    .ToList();
            }
            else if (userRole == "Recruiter")
            {
                return _context.Companies
                    .Where(c => !c.CO_NAME.Contains("Universitatea"))
                    .Order()
                    .ToList();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<COMPANY>> GetCompanyById(Guid id)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpPost]
        public async Task<ActionResult<COMPANY>> CreateCompany([FromBody] COMPANY request)
        {
            request.CO_ID = Guid.NewGuid();
            _context.Companies.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompanyById", new { id = request.CO_ID }, request);
        }
    }
}
