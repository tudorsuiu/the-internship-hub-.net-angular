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
    }
}
