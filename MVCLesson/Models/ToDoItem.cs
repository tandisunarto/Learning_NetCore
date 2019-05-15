using System.Collections.Generic;

namespace MVCLesson.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public bool IsDone { get; set; }
    }

    public class Seed
    {
        public static List<ToDoItem> ToDoItem()
        {
            List<ToDoItem> items = new List<ToDoItem> {
                new ToDoItem {
                    Id = 1,
                    Name = "Debug duplicate user created",
                    Priority = 1,
                    IsDone = false
                },
                new ToDoItem {
                    Id = 2,
                    Name = "Create graphql object types",
                    Priority = 3,
                    IsDone = false
                },
                new ToDoItem {
                    Id = 3,
                    Name = "Deploy login service to dev",
                    Priority = 4,
                    IsDone = true
                },
                new ToDoItem {
                    Id = 4,
                    Name = "Setup git repo",
                    Priority = 2,
                    IsDone = false
                },
                new ToDoItem {
                    Id = 5,
                    Name = "Fix login crash",
                    Priority = 5,
                    IsDone = true
                },
                new ToDoItem {
                    Id = 6,
                    Name = "Create readme for token service",
                    Priority = 3,
                    IsDone = true
                },
            };

            return items;
        }
    }
}