using Microsoft.EntityFrameworkCore;

namespace MVCLesson.Models
{
    public class ToDoItemContext : DbContext
    {
        public ToDoItemContext(DbContextOptions<ToDoItemContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoItem> toDoItems { get; set; }
    }
}