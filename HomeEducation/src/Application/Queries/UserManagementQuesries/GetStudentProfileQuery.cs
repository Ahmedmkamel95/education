using AutoMapper;
using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Common.Models;
using HomeEducation.Application.Queries.UserManagementQuesries.Dtos;
using HomeEducation.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace HomeEducation.Application.Queries.UserManagementQuesries;
public class GetStudentProfileQuery : IRequest<Result<UserProfile<Student>>>
{
    public GetStudentProfileQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; }
}

public class GetStudentProfileQueryHandler : IRequestHandler<GetStudentProfileQuery, Result<UserProfile<Student>>>
{
    private readonly IHomeEducationDbContext _context;
    public GetStudentProfileQueryHandler(IHomeEducationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UserProfile<Student>>> Handle(GetStudentProfileQuery request, CancellationToken cancellationToken)
    {
        var student = _context.Students.FirstOrDefault(u => u.Id == request.UserId);

        if (student != null)
        {
            UserProfile<Student> profile = new UserProfile<Student>() { User = student };
            return Result<UserProfile<Student>>.Success(profile);
        }

        return Result<UserProfile<Student>>.Failure(new string[] { "no user found to return profile" });

    }
}

