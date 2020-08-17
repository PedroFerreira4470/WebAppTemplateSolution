using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IJwtGenerator
    {
        Task<string> CreateTokenAsync(User user);
    }
}
