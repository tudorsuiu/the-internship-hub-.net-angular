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
    public class InternshipsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InternshipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InternshipDTO>> GetInternshipById(Guid id)
        {
            var internship = _context.Internships.FirstOrDefault(i => i.IN_ID == id);

            if (internship == null)
            {
                return NotFound();
            }

            var recruiter = _context.Users.FirstOrDefault(u => u.US_ID == internship.IN_RECRUITER_ID);
            var company = _context.Companies.FirstOrDefault(c => c.CO_ID == recruiter.US_COMPANY_ID);

            var internshipDTO = new InternshipDTO
            {
                Id = internship.IN_ID,
                Title = internship.IN_TITLE,
                Description = internship.IN_DESCRIPTION,
                Domain = internship.IN_DOMAIN,
                Company = internship.Company,
                Recruiter = new UserDTO
                {
                    Id = recruiter.US_ID,
                    FirstName = recruiter.US_FIRST_NAME,
                    LastName = recruiter.US_LAST_NAME,
                    Email = recruiter.US_EMAIL,
                    PhoneNumber = recruiter.US_PHONE_NUMBER,
                    Company = company,
                    Role = recruiter.US_ROLE,
                    City = recruiter.US_CITY,
                    IsDeleted = recruiter.US_IS_DELETED
                },
                StartDate = internship.IN_START_DATE,
                EndDate = internship.IN_END_DATE,
                PositionsAvailable = internship.IN_POSITIONS_AVAILABLE,
                Compensation = internship.IN_COMPENSATION,
                Deadline = internship.IN_DEADLINE,
                IsDeleted = internship.IN_IS_DELETED
            };

            return internshipDTO;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<InternshipDTO>>> GetInternships()
        {
            var userId = new Guid(User.FindFirstValue("id"));
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            var foundUser = _context.Users.FirstOrDefault(u => u.US_ID == userId);

            if (foundUser is null)
            {
                return NotFound();
            }

            var internships = new List<InternshipDTO>();

            if (userRole == "Student")
            {
                var appliedInternshipIds = _context.Applications
                    .Where(a => a.AP_STUDENT_ID == userId && a.AP_IS_DELETED == false)
                    .Select(a => a.AP_INTERNSHIP_ID)
                    .ToHashSet();

                internships = _context.Internships
                    .Where(i => i.IN_IS_DELETED == false && !appliedInternshipIds.Contains(i.IN_ID))
                    .Select(i => new InternshipDTO
                    {
                        Id = i.IN_ID,
                        Title = i.IN_TITLE,
                        Description = i.IN_DESCRIPTION,
                        Domain = i.IN_DOMAIN,
                        Company = i.Company,
                        Recruiter = new UserDTO
                        {
                            Id = i.Recruiter.US_ID,
                            FirstName = i.Recruiter.US_FIRST_NAME,
                            LastName = i.Recruiter.US_LAST_NAME,
                            Email = i.Recruiter.US_EMAIL,
                            PhoneNumber = i.Recruiter.US_PHONE_NUMBER,
                            Company = _context.Companies.FirstOrDefault(c => c.CO_ID == i.Recruiter.US_COMPANY_ID),
                            Role = i.Recruiter.US_ROLE,
                            City = i.Recruiter.US_CITY,
                            IsDeleted = i.Recruiter.US_IS_DELETED
                        },
                        StartDate = i.IN_START_DATE,
                        EndDate = i.IN_END_DATE,
                        PositionsAvailable = i.IN_POSITIONS_AVAILABLE,
                        Compensation = i.IN_COMPENSATION,
                        Deadline = i.IN_DEADLINE,
                        IsDeleted = i.IN_IS_DELETED,
                    })
                    .ToList();
            }
            else if (userRole == "Recruiter")
            {
                internships = _context.Internships
                   .Where(i => i.Recruiter.US_ID == userId)
                   .Select(i => new InternshipDTO
                   {
                       Id = i.IN_ID,
                       Title = i.IN_TITLE,
                       Description = i.IN_DESCRIPTION,
                       Domain = i.IN_DOMAIN,
                       Company = i.Company,
                       Recruiter = new UserDTO
                       {
                           Id = i.Recruiter.US_ID,
                           FirstName = i.Recruiter.US_FIRST_NAME,
                           LastName = i.Recruiter.US_LAST_NAME,
                           Email = i.Recruiter.US_EMAIL,
                           PhoneNumber = i.Recruiter.US_PHONE_NUMBER,
                           Company = _context.Companies.FirstOrDefault(c => c.CO_ID == i.Recruiter.US_COMPANY_ID),
                           Role = i.Recruiter.US_ROLE,
                           City = i.Recruiter.US_CITY,
                           IsDeleted = i.Recruiter.US_IS_DELETED
                       },
                       StartDate = i.IN_START_DATE,
                       EndDate = i.IN_END_DATE,
                       PositionsAvailable = i.IN_POSITIONS_AVAILABLE,
                       Compensation = i.IN_COMPENSATION,
                       Deadline = i.IN_DEADLINE,
                       IsDeleted = i.IN_IS_DELETED,
                   })
                   .ToList();
            }
            else
            {
                internships = _context.Internships
                   .Select(i => new InternshipDTO
                   {
                       Id = i.IN_ID,
                       Title = i.IN_TITLE,
                       Description = i.IN_DESCRIPTION,
                       Domain = i.IN_DOMAIN,
                       Company = i.Company,
                       Recruiter = new UserDTO
                       {
                           Id = i.Recruiter.US_ID,
                           FirstName = i.Recruiter.US_FIRST_NAME,
                           LastName = i.Recruiter.US_LAST_NAME,
                           Email = i.Recruiter.US_EMAIL,
                           PhoneNumber = i.Recruiter.US_PHONE_NUMBER,
                           Company = _context.Companies.FirstOrDefault(c => c.CO_ID == i.Recruiter.US_COMPANY_ID),
                           Role = i.Recruiter.US_ROLE,
                           City = i.Recruiter.US_CITY,
                           IsDeleted = i.Recruiter.US_IS_DELETED
                       },
                       StartDate = i.IN_START_DATE,
                       EndDate = i.IN_END_DATE,
                       PositionsAvailable = i.IN_POSITIONS_AVAILABLE,
                       Compensation = i.IN_COMPENSATION,
                       Deadline = i.IN_DEADLINE,
                       IsDeleted = i.IN_IS_DELETED,
                   })
                   .ToList();
            }

            return Ok(internships);
        }

        [HttpPost]
        public async Task<ActionResult<InternshipDTO>> PostInternship(InternshipDTO request)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole != "Recruiter")
            {
                return Unauthorized();
            }

            var company = _context.Companies.FirstOrDefault(c => c.CO_ID == request.Company.CO_ID);
            var recruiter = _context.Users.FirstOrDefault(u => u.US_ID == request.Recruiter.Id);

            var internship = new INTERNSHIP
            {
                IN_ID = Guid.NewGuid(),
                IN_TITLE = request.Title,
                IN_DESCRIPTION = request.Description,
                IN_DOMAIN = request.Domain,
                IN_COMPANY_ID = company.CO_ID,
                Company = company,
                IN_RECRUITER_ID = recruiter.US_ID,
                Recruiter = recruiter,
                IN_START_DATE = request.StartDate,
                IN_END_DATE = request.EndDate,
                IN_POSITIONS_AVAILABLE = request.PositionsAvailable,
                IN_COMPENSATION = request.Compensation,
                IN_DEADLINE = request.Deadline,
                IN_IS_DELETED = request.IsDeleted
            };

            _context.Add(internship);
            await _context.SaveChangesAsync();

            request.Id = internship.IN_ID;

            return CreatedAtAction("GetInternshipById", new { id = request.Id }, request);
        }

        [HttpPut]
        public async Task<ActionResult<InternshipDTO>> PutInternship(InternshipDTO request)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole != "Recruiter")
            {
                return Unauthorized();
            }

            var internship = _context.Internships.FirstOrDefault(i => i.IN_ID == request.Id);

            if (internship == null)
            {
                return NotFound();
            }

            var company = _context.Companies.FirstOrDefault(c => c.CO_ID == request.Company.CO_ID);
            var recruiter = _context.Users.FirstOrDefault(u => u.US_ID == request.Recruiter.Id);

            internship.IN_TITLE = request.Title;
            internship.IN_DESCRIPTION = request.Description;
            internship.IN_DOMAIN = request.Domain;
            internship.IN_COMPANY_ID = company.CO_ID;
            internship.Company = company;
            internship.IN_RECRUITER_ID = recruiter.US_ID;
            internship.Recruiter = recruiter;
            internship.IN_START_DATE = request.StartDate;
            internship.IN_END_DATE = request.EndDate;
            internship.IN_POSITIONS_AVAILABLE = request.PositionsAvailable;
            internship.IN_COMPENSATION = request.Compensation;
            internship.IN_DEADLINE = request.Deadline;
            internship.IN_IS_DELETED = request.IsDeleted;

            _context.Internships.Update(internship);
            await _context.SaveChangesAsync();

            return Ok(request);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInternship(Guid id)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole != "Recruiter")
            {
                return Unauthorized();
            }

            var internship = _context.Internships.FirstOrDefault(i => i.IN_ID == id);

            if (internship == null)
            {
                return NotFound();
            }

            internship.IN_IS_DELETED = true;

            _context.Internships.Update(internship);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
