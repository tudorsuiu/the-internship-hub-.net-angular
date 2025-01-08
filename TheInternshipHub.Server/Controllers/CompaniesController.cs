using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheInternshipHub.Server.Domain.Entities;
using TheInternshipHub.Server.Domain.SmartService.Domain;

namespace TheInternshipHub.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<COMPANY>> CreateCompany([FromBody] COMPANY request)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole != "University")
            {
                return Unauthorized();
            }

            request.CO_ID = Guid.NewGuid();
            _context.Companies.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompanyById", new { id = request.CO_ID }, request);
        }

        [HttpGet("register")]
        public async Task<ActionResult<ICollection<COMPANY>>> GetCompaniesForRegister()
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            return _context.Companies
                .Where(c => c.CO_NAME.Contains("Universitatea"))
                .Order()
                .ToList();
        }
    }
}
