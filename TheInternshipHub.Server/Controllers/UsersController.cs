using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheInternshipHub.Server.Domain.DTOs;
using TheInternshipHub.Server.Domain.SmartService.Domain;

namespace TheInternshipHub.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(Guid id)
        {
            var user = _context.Users.FirstOrDefault(u => u.US_ID == id);

            if (user == null)
            {
                return NotFound();
            }

            var result = new UserDTO
            {
                Id = user.US_ID,
                FirstName = user.US_FIRST_NAME,
                LastName = user.US_LAST_NAME,
                Email = user.US_EMAIL,
                PhoneNumber = user.US_PHONE_NUMBER,
                Company = _context.Companies.FirstOrDefault(c => c.CO_ID == user.US_COMPANY_ID),
                Role = user.US_ROLE,
                City = user.US_CITY,
                IsDeleted = user.US_IS_DELETED
            };

            return Ok(result);
        }

        [HttpGet("logged-user")]
        public async Task<ActionResult<UserDTO>> GetLoggedUser()
        {
            var userId = new Guid(User.FindFirstValue("id"));
            var user = _context.Users.FirstOrDefault(u => u.US_ID == userId);

            if (user == null)
            {
                return NotFound();
            }

            var result = new UserDTO
            {
                Id = user.US_ID,
                FirstName = user.US_FIRST_NAME,
                LastName = user.US_LAST_NAME,
                Email = user.US_EMAIL,
                PhoneNumber = user.US_PHONE_NUMBER,
                Company = _context.Companies.FirstOrDefault(c => c.CO_ID == user.US_COMPANY_ID),
                Role = user.US_ROLE,
                City = user.US_CITY,
                IsDeleted = user.US_IS_DELETED
            };

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<UserDTO>> PutUser(UserDTO request)
        {
            var user = _context.Users.FirstOrDefault(u => u.US_ID == request.Id);

            if (user == null)
            {
                return NotFound();
            }

            var company = _context.Companies.FirstOrDefault(c => c.CO_ID == request.Company.CO_ID);

            user.US_FIRST_NAME = request.FirstName;
            user.US_LAST_NAME = request.LastName;
            user.US_EMAIL = request.Email;
            user.US_PHONE_NUMBER = request.PhoneNumber;
            user.US_COMPANY_ID = company.CO_ID;
            user.US_ROLE = request.Role;
            user.US_CITY = request.City;
            user.US_IS_DELETED = request.IsDeleted;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(request);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole != "Recruiter")
            {
                return Unauthorized();
            }

            var user = _context.Users.FirstOrDefault(u => u.US_ID == id);

            if (user == null)
            {
                return NotFound();
            }

            user.US_IS_DELETED = true;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("students")]
        public async Task<ActionResult<UserDTO>> GetStudents()
        {
            var userId = new Guid(User.FindFirstValue("id"));
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole != "University")
            {
                return Unauthorized();
            }

            var universityId = _context.Users.FirstOrDefault(u => u.US_ID == userId).US_COMPANY_ID;
            var university = _context.Companies.FirstOrDefault(c => c.CO_ID == universityId);

            if (university == null)
            {
                return NotFound();
            }

            var students = _context.Users.Where(u => u.US_IS_DELETED == false && u.US_ROLE == "Student" && u.US_COMPANY_ID == universityId).ToList();

            var statusOrder = new List<string>
             {
                "Applied",
                "In review",
                "Technical test phase",
                "Interview phase",
                "Accepted",
                "Confirmed"
            };

            var result = new List<UniversityStudentDTO>();

            foreach (var student in students)
            {
                var applications = _context.Applications
                    .Where(a => a.AP_IS_DELETED == false && a.AP_STUDENT_ID == student.US_ID && a.AP_STATUS != "Declined" && a.AP_STATUS != "Rejected")
                    .ToList();

                var mostAdvancedApplication = applications
                    .OrderBy(a => statusOrder.IndexOf(a.AP_STATUS))
                    .FirstOrDefault();

                result.Add(new UniversityStudentDTO()
                {
                    StudentId = student.US_ID,
                    StudentFirstName = student.US_FIRST_NAME,
                    StudentLastName = student.US_LAST_NAME,
                    StudentEmail = student.US_EMAIL,
                    StudentPhoneNumber = student.US_PHONE_NUMBER,
                    University = university.CO_NAME,
                    ApplicationId = mostAdvancedApplication is null ? Guid.Empty : mostAdvancedApplication.AP_ID,
                    InternshipId = mostAdvancedApplication is null ? Guid.Empty : mostAdvancedApplication.AP_INTERNSHIP_ID,
                    ApplicationStatus = mostAdvancedApplication is null ? "" : mostAdvancedApplication.AP_STATUS,
                    ApplicationCVFilePath = mostAdvancedApplication is null ? "" : mostAdvancedApplication.AP_CV_FILE_PATH
                });
            }

            return Ok(result);
        }
    }
}
