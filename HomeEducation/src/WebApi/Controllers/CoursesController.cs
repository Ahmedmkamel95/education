using HomeEducation.Application.Commands.AdminCommands;
using HomeEducation.Application.Commands.CourseCommands.Dtos;
using HomeEducation.Application.Common.Models;
using HomeEducation.Application.Queries.Courses;
using HomeEducation.Application.Queries.Courses.Dtos;
using HomeEducation.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeEducation.WebApi.Controllers;

public class CoursesController : ApiControllerBase
{

    public CoursesController() { }

    [Authorize(Roles = $"{Role.Admin},{Role.Teacher}")]
    [Route("createCourse")]
    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody]CreateCourseRequestDto request)
    {
        return Ok(await Mediator.Send(new CreateCourseCommand(request)));
    }

    [Authorize(Roles = $"{Role.Admin},{Role.Student}")]
    [Route("studentCourses")]
    [Produces(typeof(Result<List<StudentCourseResponseDto>>))]
    [HttpGet]
    public async Task<IActionResult> GetStudentCourses([FromQuery] string studentId)
    {
        return Ok(await Mediator.Send(new GetStudentCoursesQuery(studentId)));
    }
}
