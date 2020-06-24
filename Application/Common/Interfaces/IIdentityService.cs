using Application.Common.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<(Result Result, string UserId)> CreateUserAsync(User user, string password);
        Task<Result> DeleteUserAsync(string userId);
        Task<(User, SignInResult)> SignInAsync(string email, string password);
    }
}
