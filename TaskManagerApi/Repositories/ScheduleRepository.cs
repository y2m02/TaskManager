using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;

namespace TaskManagerApi.Repositories
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<Schedule>> GetAll();
        Task<Schedule> GetById(int id);
        Task Create(Schedule entity);
        Task Update(Schedule entity);
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
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Schedule> GetById(int id)
        {
            return await Context.Schedules
                .Include(w => w.Status)
                .Include(w => w.Assignment)
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
            });

            await Save();
        }
    }
}
