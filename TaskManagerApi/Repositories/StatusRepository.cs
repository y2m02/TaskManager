using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;

namespace TaskManagerApi.Repositories
{
    public interface IStatusRepository
    {
        Task<IEnumerable<Status>> GetAll();
        Task<Status> GetById(int id);
        Task Create(Status entity);
        Task BatchCreate(IEnumerable<Status> entities);
        Task Update(Status entity);
        Task BatchUpdate(IEnumerable<Status> entities);
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

        public async Task BatchCreate(IEnumerable<Status> entities)
        {
            await Context.AddRangeAsync(entities);

            await Save();
        }

        public async Task<IEnumerable<Status>> GetAll()
        {
            return await Context.Statuses
                .Include(w => w.Schedules)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Status> GetById(int id)
        {
            return await Context.Statuses.FindAsync(id).ConfigureAwait(false);
        }

        public async Task Update(Status entity)
        {
            Context.Attach(entity);

            AddPropertiesToModify(entity, new List<string>
            {
                nameof(entity.Description)
            });

            await Save();
        }

        public async Task BatchUpdate(IEnumerable<Status> entities)
        {
            entities.ForAll(entity =>
            {
                Context.Attach(entity);

                AddPropertiesToModify(entity, new List<string>
                {
                    nameof(entity.Description)
                });
            });

            await Save();
        }
    }
}