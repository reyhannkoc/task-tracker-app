using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using EntityLayer;

namespace BusinessLayer
{
    public class TaskManager
    {
        private readonly TaskDbContext _context;

        public TaskManager(TaskDbContext context)
        {
            _context = context;
        }

        public void AddTask(TaskItem taskItem)
        {
            _context.TaskItems.Add(taskItem);
            _context.SaveChanges();
        }

        public List<TaskItem> GetAllTasks()
        {
            return _context.TaskItems.ToList();
        }

        public void DeleteTask(int id)
        {
            var taskItem = _context.TaskItems.FirstOrDefault(x => x.Id == id);
            if (taskItem == null)
            {
                return;
            }

            _context.TaskItems.Remove(taskItem);
            _context.SaveChanges();
        }

        public void MarkAsCompleted(int id)
        {
            var taskItem = _context.TaskItems.FirstOrDefault(x => x.Id == id);
            if (taskItem == null)
            {
                return;
            }

            taskItem.IsCompleted = true;
            _context.SaveChanges();
        }

        public List<TaskItem> GetCompletedTasks()
        {
            return _context.TaskItems.Where(x => x.IsCompleted).ToList();
        }

        public List<TaskItem> GetPendingTasks()
        {
            return _context.TaskItems.Where(x => !x.IsCompleted).ToList();
        }

        public List<TaskItem> GetTasksByCategory(string category)
        {
            return _context.TaskItems.Where(x => x.Category == category).ToList();
        }

        public List<TaskItem> GetTasksOrderByDueDate()
        {
            return _context.TaskItems.OrderBy(x => x.DueDate).ToList();
        }

        public int GetTaskCount()
        {
            return _context.TaskItems.Count();
        }
    }
}
