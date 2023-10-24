using HomeEducation.Application.Common.Models;
using HomeEducation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace HomeEducation.Application.Common.Interfaces;
public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<Result<string>> AuthenticateUserAsync(string email, string password);

    Task<(Result<string> Result, string UserId)> CreateUserAsync(string userName, string password, string role);

    Task<Result<string>> DeleteUserAsync(string userId);
}
