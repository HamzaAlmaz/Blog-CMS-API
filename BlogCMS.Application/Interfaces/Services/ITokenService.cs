using BlogCMS.Domain.Entities;

namespace BlogCMS.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}
