using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;

namespace TaskManagerApi.Repositories
{
    public interface IAssignmentRepository
    {
        Task<IEnumerable<Assignment>> GetAll();
        Task<IEnumerable<Assignment>> GetAllForDropDownList();
        Task<Assignment> GetById(int id);
        Task Create(Assignment entity);
        Task Update(Assignment entity);
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
                .Include(w => w.Store)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Assignment>> GetAllForDropDownList()
        {
            return await Context.Assignments
                .Where(w => w.Schedule == null)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Assignment> GetById(int id)
        {
            return await Context.Assignments
                .Include(w => w.Schedule)
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
                nameof(entity.StoreId),
            });

            await Save();
        }
    }
}
