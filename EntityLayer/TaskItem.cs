using System;

namespace EntityLayer
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
        public string Category { get; set; }

        public TaskItem()
        {
            Title = string.Empty;
            Category = string.Empty;
        }
    }
}
