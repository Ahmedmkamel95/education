using AutoMapper;
using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Common.Models;
using HomeEducation.Application.Queries.UserManagementQuesries.Dtos;
using HomeEducation.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace HomeEducation.Application.Queries.UserManagementQuesries;
public class GetAdminProfileQuery : IRequest<Result<UserProfile<Admin>>>
{
    public GetAdminProfileQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; }
}

public class GetAdminProfileQueryHandler : IRequestHandler<GetAdminProfileQuery, Result<UserProfile<Admin>>>
{
    private readonly IHomeEducationDbContext _context;
    public GetAdminProfileQueryHandler(IHomeEducationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UserProfile<Admin>>> Handle(GetAdminProfileQuery request, CancellationToken cancellationToken)
    {
        var Admin = _context.Admins.FirstOrDefault(u => u.Id == request.UserId);

        if (Admin != null)
        {
            UserProfile<Admin> profile = new UserProfile<Admin>() { User = Admin };
            return Result<UserProfile<Admin>>.Success(profile);
        }

        return Result<UserProfile<Admin>>.Failure(new string[] { "no user found to return profile" });

    }
}

