using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public async Task<List<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var entity = await entities.SingleOrDefaultAsync(s => s.Id == id);
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            return entity;
        }

        public async Task Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            await entities.AddAsync(entity);
            await Save();
        }

        public async Task Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            await Save();
        }

        public async Task Delete(int id)
        {

            var entity = await entities.SingleOrDefaultAsync(s => s.Id == id);
            if (entity == null) throw new ArgumentNullException("entity");

            entities.Remove(entity);

            await Save();
        }

        public async Task Save()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw new DbUpdateConcurrencyException();
            }
        }
    }
}
