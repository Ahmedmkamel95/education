using HomeEducation.Application.Commands.CourseCommands.Dtos;
using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Common.Models;
using HomeEducation.Domain.Constants;
using HomeEducation.Domain.Dtos.UserManagementDtos;
using HomeEducation.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeEducation.Application.Commands.AdminCommands;
public record CreateCourseCommand : IRequest<Result<string>>
{
    public CreateCourseCommand(CreateCourseRequestDto request)
    {
        Request = request;
    }

    public CreateCourseRequestDto Request { get; set; }
}

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result<string>>
{
    private readonly IHomeEducationDbContext _context;
    private readonly ILogger<CreateCourseCommandHandler> _logger;
    public CreateCourseCommandHandler(IHomeEducationDbContext context, ILogger<CreateCourseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<Result<string>> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
    {
        Course course = null;
        try
        {
            if (!_context.Levels.Any(level => command.Request.LevelId == level.Id))
            {
                _logger.LogError($"Faild to create Course : {command.Request.TitleEn}, Levels are wrong ");
                return Result<string>.Failure(new string[] { $"Faild to create Course : {command.Request.TitleEn}, Levels are wrong" });
            }
            if (!_context.Teachers.Any(teacher => command.Request.TeacherId == teacher.Id))
            {
                _logger.LogError($"Faild to create Course : {command.Request.TitleEn}, no existing teacher with {command.Request.TeacherId} ");
                return Result<string>.Failure(new string[] { $"Faild to create Course : {command.Request.TitleEn}, no existing teacher with {command.Request.TeacherId}" });
            }
            course = new Course
            {
                Id= Guid.NewGuid().ToString(),
                TeacherId = command.Request.TeacherId,
                LevelId = command.Request.LevelId,
                TitleEn = command.Request.TitleEn,
                TitleAr = command.Request.TitleAr,
                DescriptionAr = command.Request.DescriptionAr,
                DescriptionEn = command.Request.DescriptionEn,
                Image = "image"
            };

            await _context.Courses.AddAsync(course);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return Result<string>.Success("User created successfully");

        }
        catch (Exception ex)
        {
            _logger.LogError($"Faild to create course {course?.Id}: {course?.TitleEn} ");
            _logger.LogError(ex.Message);

            return Result<string>.Failure(new string[] { $"Faild to create course {course?.Id}: {course?.TitleEn} " });
        }
    }
}
