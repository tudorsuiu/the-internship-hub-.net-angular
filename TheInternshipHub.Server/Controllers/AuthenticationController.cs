using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheInternshipHub.Server.Domain.DTOs;
using TheInternshipHub.Server.Domain.Entities;
using TheInternshipHub.Server.Domain.Interfaces;
using TheInternshipHub.Server.Domain.SmartService.Domain;

namespace TheInternshipHub.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly ITokenService _tokenService;

        public AuthenticationController(ApplicationDbContext context, IPasswordHasherService passwordHasherService, ITokenService tokenService)
        {
            _context = context;
            _passwordHasherService = passwordHasherService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserLoginDTO request)
        {
            var foundUser = _context.Users.FirstOrDefault(u => u.US_EMAIL == request.Email);
            if (foundUser is null || !_passwordHasherService.Verify(foundUser.US_PASSWORD, request.Password))
            {
                return NotFound("Wrong credentials!");
            }

            var company = _context.Companies.FirstOrDefault(c => c.CO_ID == foundUser.US_COMPANY_ID);

            var token = _tokenService.GenerateToken(foundUser);
            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRegisterDTO request)
        {
            if (_context.Users.Any(u => u.US_EMAIL == request.Email))
            {
                return Conflict("This email is already used!");
            }

            var hashedPassword = _passwordHasherService.Hash(request.Password);
            var newUser = new USER
            {
                US_ID = Guid.NewGuid(),
                US_FIRST_NAME = request.FirstName,
                US_LAST_NAME = request.LastName,
                US_EMAIL = request.Email,
                US_PHONE_NUMBER = request.PhoneNumber,
                US_COMPANY_ID = request.CompanyId,
                US_PASSWORD = hashedPassword,
                US_ROLE = request.Role,
                US_CITY = request.City,
                US_IS_DELETED = false
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
