using SalesProject.Domain.Entities;

namespace SalesProject.Domain.Interfaces.Service
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}