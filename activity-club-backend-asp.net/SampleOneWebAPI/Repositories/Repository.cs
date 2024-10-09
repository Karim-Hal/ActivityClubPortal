using Microsoft.EntityFrameworkCore;
using SampleOneWebAPI.Data;
using SampleOneWebAPI.Repositories.Interfaces;

namespace SampleOneWebAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ActivityPortalDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(ActivityPortalDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();

            }

        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var entities = await _dbSet.ToListAsync();
            return entities;


        }

        public async Task<T> GetById(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;

        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckEntityExists(int id)
        {
            return await _dbSet.FindAsync(id) != null;
        }
    }
}
