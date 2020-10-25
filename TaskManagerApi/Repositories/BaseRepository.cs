using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagerApi.Models;

namespace TaskManagerApi.Repositories
{
    public class BaseRepository
    {
        protected TaskManagerContext Context { get; }

        protected BaseRepository(TaskManagerContext context)
        {
            Context = context;
        }

        protected async Task Save()
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
