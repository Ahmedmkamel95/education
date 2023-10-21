using HomeEducation.Application.Common.Models;
using HomeEducation.Domain.Entities;

namespace HomeEducation.Application.Common.Interfaces;
public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<Result<string>> AuthenticateUserAsync(string email, string password, string userType);

    Task<(Result<string> Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result<string>> DeleteUserAsync(string userId);
}
