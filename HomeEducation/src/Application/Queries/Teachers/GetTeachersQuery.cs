using AutoMapper;
using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Common.Models;
using HomeEducation.Application.Queries.GetLevels;
using HomeEducation.Application.Queries.Teachers;
using HomeEducation.Domain.Entities;
using HomeEducation.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace HomeEducation.Application.Levels.Quesries;
public record GetTeachersQuery : IRequest<Result<List<GetTeachersResponseDto>>>
{
   
}

public class GetTeachersQueryHandler : IRequestHandler<GetTeachersQuery, Result<List<GetTeachersResponseDto>>>
{
    private readonly IHomeEducationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IOptions<RequestLocalizationOptions> _options;

    public GetTeachersQueryHandler(IHomeEducationDbContext context, IMapper mapper, IOptions<RequestLocalizationOptions> options)
    {
        _context = context;
        _mapper = mapper;
        _options = options;
    }

    public async Task<Result<List<GetTeachersResponseDto>>> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
    {
        var teachers = _context.Teachers.ToList();
        var teacherResults = _mapper.Map<List<GetTeachersResponseDto>>(teachers);
        return Result<List<GetTeachersResponseDto>>.Success(teacherResults);
    }
}
