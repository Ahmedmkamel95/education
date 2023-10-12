using AutoMapper;
using HomeEducation.Application.Common.Interfaces;
using HomeEducation.Application.Queries.GetLevels;
using HomeEducation.Domain.Enums;
using MediatR;

namespace HomeEducation.Application.TodoItems.Queries.GetTodoItemsWithPagination;
public record GetLevelsQuery : IRequest<GetLevelsDto>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetLevelsQueryHandler : IRequestHandler<GetLevelsQuery, GetLevelsDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLevelsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetLevelsDto> Handle(GetLevelsQuery request, CancellationToken cancellationToken)
    {
        return new GetLevelsDto
        { 
            Primary = _mapper.Map<List<PhaseGradeDto>>(_context.Levels.Where(x => x.Phase == StudyPhase.Primary)),
            Preparatory = _mapper.Map<List<PhaseGradeDto>>(_context.Levels.Where(x => x.Phase == StudyPhase.Preparatory)),
            Secondary = _mapper.Map<List<PhaseGradeDto>>(_context.Levels.Where(x => x.Phase == StudyPhase.Secondary)),
        };
    }
}
