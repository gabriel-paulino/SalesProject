using SalesProject.Domain.Entities;

namespace SalesProject.Domain.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}