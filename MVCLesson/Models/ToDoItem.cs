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
}