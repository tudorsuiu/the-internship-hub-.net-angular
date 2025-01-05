using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TheInternshipHub.Server.Domain.DTOs;
using TheInternshipHub.Server.Domain.Entities;
using TheInternshipHub.Server.Domain.Interfaces;
using TheInternshipHub.Server.Domain.SmartService.Domain;
using TheInternshipHub.Server.Services;

namespace TheInternshipHub.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAzureBlobStorageService _blobStorageService;

        public ApplicationsController(ApplicationDbContext context, IAzureBlobStorageService blobStorageService)
        {
            _context = context;
            _blobStorageService = blobStorageService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationDTO>> GetApplicationById(Guid id)
        {
            var application = _context.Applications.FirstOrDefault(i => i.AP_ID == id);

            if (application == null)
            {
                return NotFound();
            }

            var internship = _context.Internships.FirstOrDefault(i => i.IN_ID == application.Internship.IN_ID);
            var student = _context.Users.First(u => u.US_ID == application.Student.US_ID);

            var applicationDTO = new ApplicationDTO
            {
                Id = application.AP_ID,
                InternshipId = application.Internship.IN_ID,
                Internship = new InternshipDTO
                {
                    Id = internship.IN_ID,
                    Title = internship.IN_TITLE,
                    Description = internship.IN_DESCRIPTION,
                    Company = internship.Company,
                    Recruiter = new UserDTO
                    {
                        Id = internship.Recruiter.US_ID,
                        FirstName = internship.Recruiter.US_FIRST_NAME,
                        LastName = internship.Recruiter.US_LAST_NAME,
                        Email = internship.Recruiter.US_EMAIL,
                        PhoneNumber = internship.Recruiter.US_PHONE_NUMBER,
                        Company = _context.Companies.FirstOrDefault(c => c.CO_ID == internship.Recruiter.US_COMPANY_ID),
                        Role = internship.Recruiter.US_ROLE,
                        City = internship.Recruiter.US_CITY,
                        IsDeleted = internship.Recruiter.US_IS_DELETED
                    },
                    StartDate = internship.IN_START_DATE,
                    EndDate = internship.IN_END_DATE,
                    PositionsAvailable = internship.IN_POSITIONS_AVAILABLE,
                    Compensation = internship.IN_COMPENSATION,
                    IsDeleted = internship.IN_IS_DELETED
                },
                StudentId = application.Student.US_ID,
                Student = new UserDTO
                {
                    Id = student.US_ID,
                    FirstName = student.US_FIRST_NAME,
                    LastName = student.US_LAST_NAME,
                    Email = student.US_EMAIL,
                    PhoneNumber = student.US_PHONE_NUMBER,
                    Company = _context.Companies.FirstOrDefault(c => c.CO_ID == student.US_COMPANY_ID),
                    Role = student.US_ROLE,
                    City = student.US_CITY,
                    IsDeleted = student.US_IS_DELETED
                },
                AppliedDate = application.AP_APPLIED_DATE,
                Status = application.AP_STATUS,
                CVFilePath = application.AP_CV_FILE_PATH,
                IsDeleted = application.AP_IS_DELETED
            };

            return applicationDTO;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ApplicationDTO>>> GetApplications()
        {
            var userId = new Guid(User.FindFirstValue("id"));
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole == "Student")
            {
                var applications = _context.Applications
                    .Where(a => a.AP_STUDENT_ID == userId);

                var applicationDTOs = applications.Select(application => new ApplicationDTO
                {
                    Id = application.AP_ID,
                    InternshipId = application.Internship.IN_ID,
                    Internship = new InternshipDTO
                    {
                        Id = application.Internship.IN_ID,
                        Title = application.Internship.IN_TITLE,
                        Description = application.Internship.IN_DESCRIPTION,
                        Company = application.Internship.Company,
                        Recruiter = new UserDTO
                        {
                            Id = application.Internship.Recruiter.US_ID,
                            FirstName = application.Internship.Recruiter.US_FIRST_NAME,
                            LastName = application.Internship.Recruiter.US_LAST_NAME,
                            Email = application.Internship.Recruiter.US_EMAIL,
                            PhoneNumber = application.Internship.Recruiter.US_PHONE_NUMBER,
                            Company = _context.Companies.FirstOrDefault(c => c.CO_ID == application.Internship.Recruiter.US_COMPANY_ID),
                            Role = application.Internship.Recruiter.US_ROLE,
                            City = application.Internship.Recruiter.US_CITY,
                            IsDeleted = application.Internship.Recruiter.US_IS_DELETED
                        },
                        StartDate = application.Internship.IN_START_DATE,
                        EndDate = application.Internship.IN_END_DATE,
                        PositionsAvailable = application.Internship.IN_POSITIONS_AVAILABLE,
                        Compensation = application.Internship.IN_COMPENSATION,
                        IsDeleted = application.Internship.IN_IS_DELETED
                    },
                    StudentId = application.Student.US_ID,
                    Student = new UserDTO
                    {
                        Id = application.Student.US_ID,
                        FirstName = application.Student.US_FIRST_NAME,
                        LastName = application.Student.US_LAST_NAME,
                        Email = application.Student.US_EMAIL,
                        PhoneNumber = application.Student.US_PHONE_NUMBER,
                        Company = _context.Companies.FirstOrDefault(c => c.CO_ID == application.Student.US_COMPANY_ID),
                        Role = application.Student.US_ROLE,
                        City = application.Student.US_CITY,
                        IsDeleted = application.Student.US_IS_DELETED
                    },
                    AppliedDate = application.AP_APPLIED_DATE,
                    Status = application.AP_STATUS,
                    CVFilePath = application.AP_CV_FILE_PATH,
                    IsDeleted = application.AP_IS_DELETED
                }).ToList();

                return Ok(applicationDTOs);
            }
            else if (userRole == "Recruiter")
            {
                var applications = _context.Applications
                    .Where(a => a.Internship.IN_RECRUITER_ID == userId);

                var applicationDTOs = applications.Select(application => new ApplicationDTO
                {
                    Id = application.AP_ID,
                    InternshipId = application.Internship.IN_ID,
                    Internship = new InternshipDTO
                    {
                        Id = application.Internship.IN_ID,
                        Title = application.Internship.IN_TITLE,
                        Description = application.Internship.IN_DESCRIPTION,
                        Company = application.Internship.Company,
                        Recruiter = new UserDTO
                        {
                            Id = application.Internship.Recruiter.US_ID,
                            FirstName = application.Internship.Recruiter.US_FIRST_NAME,
                            LastName = application.Internship.Recruiter.US_LAST_NAME,
                            Email = application.Internship.Recruiter.US_EMAIL,
                            PhoneNumber = application.Internship.Recruiter.US_PHONE_NUMBER,
                            Company = _context.Companies.FirstOrDefault(c => c.CO_ID == application.Internship.Recruiter.US_COMPANY_ID),
                            Role = application.Internship.Recruiter.US_ROLE,
                            City = application.Internship.Recruiter.US_CITY,
                            IsDeleted = application.Internship.Recruiter.US_IS_DELETED
                        },
                        StartDate = application.Internship.IN_START_DATE,
                        EndDate = application.Internship.IN_END_DATE,
                        PositionsAvailable = application.Internship.IN_POSITIONS_AVAILABLE,
                        Compensation = application.Internship.IN_COMPENSATION,
                        IsDeleted = application.Internship.IN_IS_DELETED
                    },
                    StudentId = application.Student.US_ID,
                    Student = new UserDTO
                    {
                        Id = application.Student.US_ID,
                        FirstName = application.Student.US_FIRST_NAME,
                        LastName = application.Student.US_LAST_NAME,
                        Email = application.Student.US_EMAIL,
                        PhoneNumber = application.Student.US_PHONE_NUMBER,
                        Company = _context.Companies.FirstOrDefault(c => c.CO_ID == application.Student.US_COMPANY_ID),
                        Role = application.Student.US_ROLE,
                        City = application.Student.US_CITY,
                        IsDeleted = application.Student.US_IS_DELETED
                    },
                    AppliedDate = application.AP_APPLIED_DATE,
                    Status = application.AP_STATUS,
                    CVFilePath = application.AP_CV_FILE_PATH,
                    IsDeleted = application.AP_IS_DELETED
                }).ToList();

                return Ok(applicationDTOs);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationDTO>> PostApplication(ApplicationDTO request)
        {
            var userId = new Guid(User.FindFirstValue("id"));
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole != "Student")
            {
                return Unauthorized();
            }

            var internship = _context.Internships.FirstOrDefault(i => i.IN_ID == request.Id);
            var student = _context.Users.FirstOrDefault(u => u.US_ID == userId);

            var application = new APPLICATION
            {
                AP_ID = Guid.NewGuid(),
                AP_INTERNSHIP_ID = internship.IN_ID,
                Internship = internship,
                AP_STUDENT_ID = student.US_ID,
                Student = student,
                AP_APPLIED_DATE = request.AppliedDate,
                AP_STATUS = request.Status,
                AP_CV_FILE_PATH = request.CVFilePath,
                AP_IS_DELETED = request.IsDeleted
            };

            _context.Add(application);
            await _context.SaveChangesAsync();

            request.Id = application.AP_ID;

            return CreatedAtAction("GetApplicationById", new { id = request.Id }, request);
        }

        [HttpPut]
        public async Task<ActionResult<ApplicationDTO>> PutApplication(ApplicationDTO request)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (userRole != "Recruiter")
            {
                return Unauthorized();
            }

            var application = _context.Applications.FirstOrDefault(a => a.AP_ID == request.Id);

            if (application == null)
            {
                return NotFound();
            }

            application.AP_STATUS = request.Status;

            _context.Applications.Update(application);
            await _context.SaveChangesAsync();

            return Ok(request);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(Guid id)
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
