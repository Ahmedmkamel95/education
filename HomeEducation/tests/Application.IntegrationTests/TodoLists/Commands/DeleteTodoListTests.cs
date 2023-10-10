using HomeEducation.Application.Common.Exceptions;
using HomeEducation.Application.TodoLists.Commands.CreateTodoList;
using HomeEducation.Application.TodoLists.Commands.DeleteTodoList;
using HomeEducation.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

using static HomeEducation.Application.IntegrationTests.Testing;

namespace HomeEducation.Application.IntegrationTests.TodoLists.Commands;
public class DeleteTodoListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new DeleteTodoListCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoList()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        await SendAsync(new DeleteTodoListCommand(listId));

        var list = await FindAsync<TodoList>(listId);

        list.Should().BeNull();
    }
}
