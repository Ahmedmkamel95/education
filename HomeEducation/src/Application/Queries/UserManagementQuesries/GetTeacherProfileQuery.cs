using AutoMapper;
using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Common.Models;
using HomeEducation.Application.Queries.UserManagementQuesries.Dtos;
using HomeEducation.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace HomeEducation.Application.Queries.UserManagementQuesries;
public class GetTeacherProfileQuery : IRequest<Result<UserProfile<Teacher>>>
{
    public GetTeacherProfileQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; }
}

public class GetTeacherProfileQueryHandler : IRequestHandler<GetTeacherProfileQuery, Result<UserProfile<Teacher>>>
{
    private readonly IHomeEducationDbContext _context;
    public GetTeacherProfileQueryHandler(IHomeEducationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UserProfile<Teacher>>> Handle(GetTeacherProfileQuery request, CancellationToken cancellationToken)
    {
        var Teacher = _context.Teachers.FirstOrDefault(u => u.Id == request.UserId);

        if (Teacher != null)
        {
            UserProfile<Teacher> profile = new UserProfile<Teacher>() { User = Teacher };
            return Result<UserProfile<Teacher>>.Success(profile);
        }

        return Result<UserProfile<Teacher>>.Failure(new string[] { "no user found to return profile" });

    }
}

