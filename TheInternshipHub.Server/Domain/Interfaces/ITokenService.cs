using TheInternshipHub.Server.Domain.Entities;

namespace TheInternshipHub.Server.Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(USER user);
    }
}
