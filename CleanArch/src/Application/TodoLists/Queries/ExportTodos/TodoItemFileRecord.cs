using CleanArch.Application.Common.Mappings;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
