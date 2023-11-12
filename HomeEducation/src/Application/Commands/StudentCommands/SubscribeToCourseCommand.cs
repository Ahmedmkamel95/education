using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HomeEducation.Application.Commands.AdminCommands;
using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Common.Models;
using HomeEducation.Application.Queries.Courses.Dtos;
using HomeEducation.Application.Queries.Courses;
using HomeEducation.Domain.Constants;
using HomeEducation.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace HomeEducation.Application.Commands.StudentCommands;
public class SubscribeToCourseCommand : IRequest<Result<string>>
{
    public SubscribeToCourseCommand(string courseId)
    {
        CourseId = courseId;
    }

    public string CourseId { get; set; }

}

public class SubscribeToCourseHandler : IRequestHandler<SubscribeToCourseCommand, Result<string>>
{
    private readonly IHomeEducationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IOptions<RequestLocalizationOptions> _options;
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<SubscribeToCourseHandler> _logger;

    public SubscribeToCourseHandler(IHomeEducationDbContext context, IMapper mapper, IOptions<RequestLocalizationOptions> options, IIdentityService identityService, ICurrentUserService currentUserService, ILogger<SubscribeToCourseHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _options = options;
        _identityService = identityService;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(SubscribeToCourseCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? string.Empty;
        try
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var student = _context.Students.FirstOrDefault(s => s.Id == userId);
                var course = _context.Courses.FirstOrDefault(s => s.Id == request.CourseId);
                if (student == null || course == null)
                {
                    return Result<string>.Failure(new string[] { $"Wrong student: {userId} or wrong course {request.CourseId} not found" });
                }
                if (student.LevelId != course.LevelId)
                {
                    return Result<string>.Failure(new string[] { $"student level doesn't match course level" });
                }
                var studentCourse = new StudentCourseEnrollment() { CourseId = request.CourseId, StudentId = userId };
                _context.StudentCourseEnrollments.Add(studentCourse);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<string>.Success("course subscribed successfully");
            }

            _logger.LogError($"Wrong student: {userId}");
            return Result<string>.Failure(new string[] { $"Wrong student: {userId}" });
        }
        catch (Exception ex)
        {
            _logger.LogError($"error while subscribing to course ${request.CourseId} by student: {userId}");
            return Result<string>.Failure(new string[] { $"error while subscribing to course ${request.CourseId} by student: {userId}" });
        }
    }

}

