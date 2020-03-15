using Domain.Entities;

namespace Application._Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(User user);
    }
}
