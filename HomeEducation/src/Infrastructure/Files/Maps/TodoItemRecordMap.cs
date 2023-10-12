using System.Globalization;
using CsvHelper.Configuration;
using HomeEducation.Application.TodoLists.Queries.ExportTodos;

namespace HomeEducation.Infrastructure.Files.Maps;
public class TodoItemRecordMap : ClassMap<TodoItemRecord>
{
    public TodoItemRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Done).Convert(c => c.Value.Done ? "Yes" : "No");
    }
}
