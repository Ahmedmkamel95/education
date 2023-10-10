using HomeEducation.Application.Common.Mappings;
using HomeEducation.Domain.Entities;

namespace HomeEducation.Application.TodoLists.Queries.ExportTodos;
public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; init; }

    public bool Done { get; init; }
}
