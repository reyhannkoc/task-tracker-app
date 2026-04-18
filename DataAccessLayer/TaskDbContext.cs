using System.Data.Entity;
using EntityLayer;

namespace DataAccessLayer
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext()
            : base("name=TaskDbContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<TaskDbContext>());
            Database.Initialize(false);
        }

        public virtual DbSet<TaskItem> TaskItems { get; set; }
    }
}
