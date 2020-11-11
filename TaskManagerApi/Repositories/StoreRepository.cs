using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;

namespace TaskManagerApi.Repositories
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetAll();
        Task<Store> GetById(int id);
        Task Create(Store entity);
        Task BatchCreate(IEnumerable<Store> entities);
        Task Update(Store entity);
        Task BatchUpdate(IEnumerable<Store> entities);
        Task Delete(Store entity);
        Task BatchDelete(IEnumerable<Store> entities);
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

        public async Task BatchCreate(IEnumerable<Store> entities)
        {
            await Context.AddRangeAsync(entities);

            await Save();
        }

        public async Task<IEnumerable<Store>> GetAll()
        {
            return await Context.Stores
                .Include(w => w.Assignments)
                .OrderBy(w => w.Name)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Store> GetById(int id)
        {
            return await Context.Stores.FindAsync(id).ConfigureAwait(false);
        }

        public async Task Update(Store entity)
        {
            Context.Attach(entity);

            AddPropertiesToModify(entity, new List<string>
            {
                nameof(entity.Name)
            });

            await Save();
        }

        public async Task BatchUpdate(IEnumerable<Store> entities)
        {
            entities.ForAll(entity =>
            {
                Context.Attach(entity);

                AddPropertiesToModify(entity, new List<string>
                {
                    nameof(entity.Name)
                });
            });

            await Save();
        }

        public async Task Delete(Store entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;

            await Save();
        }

        public async Task BatchDelete(IEnumerable<Store> entities)
        {
            entities.ForAll(entity => { Context.Entry(entity).State = EntityState.Deleted; });

            await Save();
        }
    }
}