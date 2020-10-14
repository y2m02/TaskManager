using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetAll();
        Task<Store> GetById(int id);
        Task Create(Store entity);
        Task Update(Store entity);
    }

    public class StoreRepository : BaseRepository, IStoreRepository
    {
        public StoreRepository(TaskManagerContext context) : base(context)
        {

        }

        public async Task Create(Store entity)
        {
            await Context.AddAsync(entity);

            await Save();
        }

        public async Task<IEnumerable<Store>> GetAll()
        {
            return await Context.Stores.ToListAsync().ConfigureAwait(false);;
        }

        public async Task<Store> GetById(int id)
        {
            return await Context.Stores.FindAsync(id).ConfigureAwait(false);;
        }

        public async Task Update(Store entity)
        {
            Context.Attach(entity);

            AddPropertiesToModify(entity, new List<string>
            {
                nameof(entity.Name),
            });

            await Save();
        }
    }
}
