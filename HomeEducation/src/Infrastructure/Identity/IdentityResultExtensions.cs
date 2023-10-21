using HomeEducation.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace HomeEducation.Infrastructure.Identity;
public static class IdentityResultExtensions
{
    public static Result<string> ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result<string>.Success(null)
            : Result<string>.Failure(result.Errors.Select(e => e.Description).ToArray());
    }
}
