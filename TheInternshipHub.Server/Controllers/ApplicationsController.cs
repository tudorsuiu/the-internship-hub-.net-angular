﻿using Microsoft.AspNetCore.Authorization;
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
    public class ApplicationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApplicationsController(ApplicationDbContext context)
        {
            _context = context;
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
            var company = _context.Companies.FirstOrDefault(c => c.CO_ID == internship.IN_RECRUITER_ID);
            var university = _context.Companies.FirstOrDefault(c => c.CO_ID == student.US_COMPANY_ID);

            var applicationDTO = new ApplicationDTO
            {
                Id = application.AP_ID,
                InternshipId = application.Internship.IN_ID,
                Internship = new InternshipDTO
                {
                    Id = internship.IN_ID,
                    Title = internship.IN_TITLE,
                    Description = internship.IN_DESCRIPTION,
                    Domain = internship.IN_DOMAIN,
                    Company = internship.Company,
                    Recruiter = new UserDTO
                    {
                        Id = internship.Recruiter.US_ID,
                        FirstName = internship.Recruiter.US_FIRST_NAME,
                        LastName = internship.Recruiter.US_LAST_NAME,
                        Email = internship.Recruiter.US_EMAIL,
                        PhoneNumber = internship.Recruiter.US_PHONE_NUMBER,
                        Company = company,
                        Role = internship.Recruiter.US_ROLE,
                        City = internship.Recruiter.US_CITY,
                        IsDeleted = internship.Recruiter.US_IS_DELETED
                    },
                    StartDate = internship.IN_START_DATE,
                    EndDate = internship.IN_END_DATE,
                    PositionsAvailable = internship.IN_POSITIONS_AVAILABLE,
                    Compensation = internship.IN_COMPENSATION,
                    Deadline = internship.IN_DEADLINE,
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
                    Company = university,
                    Role = student.US_ROLE,
                    City = student.US_CITY,
                    IsDeleted = student.US_IS_DELETED
                },
                AppliedDate = application.AP_APPLIED_DATE,
                Status = application.AP_STATUS,
                CVFilePath = application.AP_CV_FILE_PATH,
                UniversityDocsFilePath = application.AP_UNIVERSITY_DOCS_FILE_PATH,
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
                .Where(a => a.AP_STUDENT_ID == userId)
                .ToList();

                var student = _context.Users.FirstOrDefault(u => u.US_ID == userId);

                var applicationDTOs = new List<ApplicationDTO>();

                foreach(var application in applications)
                {
                    var internship = _context.Internships.FirstOrDefault(i => i.IN_ID == application.AP_INTERNSHIP_ID);
                    var recruiter = _context.Users.FirstOrDefault(u => u.US_ID == internship.IN_RECRUITER_ID);

                    applicationDTOs.Add(new ApplicationDTO
                    {
                        Id = application.AP_ID,
                        InternshipId = application.AP_INTERNSHIP_ID,
                        Internship = new InternshipDTO
                        {
                            Id = application.AP_INTERNSHIP_ID,
                            Title = internship.IN_TITLE,
                            Description = internship.IN_DESCRIPTION,
                            Domain = internship.IN_DOMAIN,
                            Company = _context.Companies.FirstOrDefault(c => c.CO_ID == internship.IN_COMPANY_ID),
                            Recruiter = new UserDTO
                            {
                                Id = recruiter.US_ID,
                                FirstName = recruiter.US_FIRST_NAME,
                                LastName = recruiter.US_LAST_NAME,
                                Email = recruiter.US_EMAIL,
                                PhoneNumber = recruiter.US_PHONE_NUMBER,
                                Company = _context.Companies.FirstOrDefault(c => c.CO_ID == recruiter.US_COMPANY_ID),
                                Role = recruiter.US_ROLE,
                                City = recruiter.US_CITY,
                                IsDeleted = recruiter.US_IS_DELETED
                            },
                            StartDate = internship.IN_START_DATE,
                            EndDate = internship.IN_END_DATE,
                            PositionsAvailable = internship.IN_POSITIONS_AVAILABLE,
                            Compensation = internship.IN_COMPENSATION,
                            IsDeleted = internship.IN_IS_DELETED
                        },
                        StudentId = application.AP_STUDENT_ID,
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
                        UniversityDocsFilePath = application.AP_UNIVERSITY_DOCS_FILE_PATH,
                        IsDeleted = application.AP_IS_DELETED
                    });
                }

                return Ok(applicationDTOs);
            }
            else if (userRole == "Recruiter")
            {
                var applications = _context.Applications
                    .Where(a => a.Internship.IN_RECRUITER_ID == userId)
                    .ToList();

                var recruiter = _context.Users.FirstOrDefault(u => u.US_ID == userId);

                var applicationDTOs = new List<ApplicationDTO>(); 
                
                foreach (var application in applications)
                {
                    var internship = _context.Internships.FirstOrDefault(i => i.IN_ID == application.AP_INTERNSHIP_ID);
                    var student = _context.Users.FirstOrDefault(u => u.US_ID == application.AP_STUDENT_ID);

                    applicationDTOs.Add(new ApplicationDTO
                    {
                        Id = application.AP_ID,
                        InternshipId = application.AP_INTERNSHIP_ID,
                        Internship = new InternshipDTO
                        {
                            Id = application.AP_INTERNSHIP_ID,
                            Title = internship.IN_TITLE,
                            Description = internship.IN_DESCRIPTION,
                            Domain = internship.IN_DOMAIN,
                            Company = _context.Companies.FirstOrDefault(c => c.CO_ID == recruiter.US_COMPANY_ID),
                            Recruiter = new UserDTO
                            {
                                Id = recruiter.US_ID,
                                FirstName = recruiter.US_FIRST_NAME,
                                LastName = recruiter.US_LAST_NAME,
                                Email = recruiter.US_EMAIL,
                                PhoneNumber = recruiter.US_PHONE_NUMBER,
                                Company = _context.Companies.FirstOrDefault(c => c.CO_ID == recruiter.US_COMPANY_ID),
                                Role = recruiter.US_ROLE,
                                City = recruiter.US_CITY,
                                IsDeleted = recruiter.US_IS_DELETED
                            },
                            StartDate = internship.IN_START_DATE,
                            EndDate = internship.IN_END_DATE,
                            PositionsAvailable = internship.IN_POSITIONS_AVAILABLE,
                            Compensation = internship.IN_COMPENSATION,
                            IsDeleted = internship.IN_IS_DELETED
                        },
                        StudentId = application.AP_STUDENT_ID,
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
                        UniversityDocsFilePath = application.AP_UNIVERSITY_DOCS_FILE_PATH,
                        IsDeleted = application.AP_IS_DELETED
                    });
                }

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

            var internship = _context.Internships.FirstOrDefault(i => i.IN_ID == request.InternshipId);
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
                AP_UNIVERSITY_DOCS_FILE_PATH = request.UniversityDocsFilePath,
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
            var application = _context.Applications.FirstOrDefault(a => a.AP_ID == request.Id);

            if (application == null)
            {
                return NotFound();
            }

            application.AP_STATUS = request.Status;
            application.AP_CV_FILE_PATH = request.CVFilePath;
            application.AP_UNIVERSITY_DOCS_FILE_PATH = request.UniversityDocsFilePath;
            application.AP_IS_DELETED = request.IsDeleted;

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
