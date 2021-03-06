using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagerApi.Models;

namespace TaskManagerApi.Repositories
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<Schedule>> GetAll();
        Task<Schedule> GetById(int id);
        Task Create(Schedule entity);
        Task Update(Schedule entity);
        Task Delete(Schedule entity);
    }

    public class ScheduleRepository : BaseRepository, IScheduleRepository
    {
        public ScheduleRepository(TaskManagerContext context) : base(context)
        {

        }

        public async Task Create(Schedule entity)
        {
            await Context.AddAsync(entity);

            await Save();
        }

        public async Task<IEnumerable<Schedule>> GetAll()
        {
            return await Context.Schedules
                .Include(w => w.Status)
                .Include(w => w.Assignment)
                .ThenInclude(w => w.Store)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Schedule> GetById(int id)
        {
            return await Context.Schedules
                .Include(w => w.Status)
                .Include(w => w.Assignment)
                .ThenInclude(w => w.Store)
                .SingleAsync(w => w.ScheduleId == id)
                .ConfigureAwait(false);
        }

        public async Task Update(Schedule entity)
        {
            Context.Attach(entity);

            AddPropertiesToModify(entity, new List<string>
            {
                nameof(entity.Date),
                nameof(entity.AssignmentId),
                nameof(entity.StatusId),
                nameof(entity.Note),
                nameof(entity.EndDate),
            });

            await Save();
        }

        public async Task Delete(Schedule entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;

            await Save();
        }
    }
}
