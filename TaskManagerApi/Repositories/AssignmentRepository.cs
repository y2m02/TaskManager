using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerApi.Models;

namespace TaskManagerApi.Repositories
{
    public interface IAssignmentRepository
    {
        Task<IEnumerable<Assignment>> GetAll();
        Task<IEnumerable<Assignment>> GetAllForDropDownList(int id = 0);
        Task<Assignment> GetById(int id);
        Task Create(Assignment entity);
        Task Update(Assignment entity);
        Task Delete(Assignment entity);
    }

    public class AssignmentRepository : BaseRepository, IAssignmentRepository
    {
        public AssignmentRepository(TaskManagerContext context) : base(context)
        {
        }

        public async Task Create(Assignment entity)
        {
            await Context.AddAsync(entity);

            await Save();
        }

        public async Task<IEnumerable<Assignment>> GetAll()
        {
            return await Context.Assignments
                .Include(w => w.Schedule)
                .ThenInclude(w => w.Status)
                .Include(w => w.Store)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Assignment>> GetAllForDropDownList(int id)
        {
            return await Context.Assignments
                .Include(w => w.Store)
                .Where(w => w.Schedule == null ||
                    w.Schedule != null &&
                    w.AssignmentId == id
                )
                .OrderBy(w => w.Description)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Assignment> GetById(int id)
        {
            return await Context.Assignments
                .Include(w => w.Schedule)
                .ThenInclude(w => w.Status)
                .Include(w => w.Store)
                .SingleAsync(w => w.AssignmentId == id)
                .ConfigureAwait(false);
        }

        public async Task Update(Assignment entity)
        {
            Context.Attach(entity);

            AddPropertiesToModify(entity, new List<string>
            {
                nameof(entity.Description),
                nameof(entity.StoreId)
            });

            await Save();
        }

        public async Task Delete(Assignment entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;

            await Save();
        }
    }
}