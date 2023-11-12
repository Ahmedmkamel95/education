using AutoMapper;
using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Common.Models;
using HomeEducation.Application.Levels.Quesries;
using HomeEducation.Application.Queries.Courses.Dtos;
using HomeEducation.Application.Queries.GetLevels;
using HomeEducation.Domain.Constants;
using HomeEducation.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HomeEducation.Application.Queries.Courses;
public class GetStudentCoursesQuery : IRequest<Result<List<StudentCourseResponseDto>>>
{
    public GetStudentCoursesQuery(string studentId)
    {
        StudentId = studentId;
    }

    public string StudentId { get; set; }

}

public class GetStudentCoursesHandler : IRequestHandler<GetStudentCoursesQuery, Result<List<StudentCourseResponseDto>>>
{
    private readonly IHomeEducationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IOptions<RequestLocalizationOptions> _options;
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;


    public GetStudentCoursesHandler(IHomeEducationDbContext context, IMapper mapper, IOptions<RequestLocalizationOptions> options, IIdentityService identityService, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _options = options;
        _identityService = identityService;
        _currentUserService = currentUserService;
    }

    public async Task<Result<List<StudentCourseResponseDto>>> Handle(GetStudentCoursesQuery request, CancellationToken cancellationToken)
    {
        var studentCourses = _context.Courses
            .Include(x => x.Level)
            .Include(x => x.Teacher)
            .Where(course => course.StudentCourseEnrollments.Any(s => s.StudentId == request.StudentId)).ToList();

        var userId = _currentUserService.UserId ?? string.Empty;
        if (!string.IsNullOrEmpty(userId))
        {
            string userRole = await _identityService.GetUserRole(userId);

            if (!string.IsNullOrEmpty(userRole) && userRole == Role.Admin)
            {
                studentCourses = studentCourses.Where(x => x.CreatedBy == userId).ToList();
            }
        }
        var response = _mapper.Map<List<StudentCourseResponseDto>>(studentCourses, opt => opt.Items["culture"] = _options.Value.DefaultRequestCulture.Culture.Name);

        return Result<List<StudentCourseResponseDto>>.Success(response);
    }
}
