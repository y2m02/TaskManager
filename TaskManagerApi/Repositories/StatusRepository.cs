using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;

namespace TaskManagerApi.Repositories
{
    public interface IStatusRepository
    {
        Task<IEnumerable<Status>> GetAll();
        Task<Status> GetById(int id);
        Task Create(Status entity);
        Task Update(Status entity);
    }

    public class StatusRepository : BaseRepository, IStatusRepository
    {
        public StatusRepository(TaskManagerContext context) : base(context)
        {

        }

        public async Task Create(Status entity)
        {
            await Context.AddAsync(entity);

            await Save();
        }

        public async Task<IEnumerable<Status>> GetAll()
        {
            return await Context.Statuses.ToListAsync().ConfigureAwait(false);;
        }

        public async Task<Status> GetById(int id)
        {
            return await Context.Statuses.FindAsync(id).ConfigureAwait(false);;
        }

        public async Task Update(Status entity)
        {
            Context.Attach(entity);

            AddPropertiesToModify(entity, new List<string>
            {
                nameof(entity.Description),
            });

            await Save();
        }
    }
}
