using System.Collections.Generic;
using TaskManager.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Repositories
{
    public class BaseRepository
    {
        protected TaskManagerContext Context { get; }

        public BaseRepository(TaskManagerContext context)
        {
            Context = context;
        }

        public async Task Save()
        {
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        protected void AddPropertiesToModify<T>(T entity, List<string> properties)
        {
            properties.ForEach(propertyName =>
            {
                Context.Entry(entity).Property(propertyName).IsModified = true;
            });
        }
    }
}
