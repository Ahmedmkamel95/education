using HomeEducation.Application.TodoLists.Queries.ExportTodos;

namespace HomeEducation.Application.Common.Interfaces;
public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
