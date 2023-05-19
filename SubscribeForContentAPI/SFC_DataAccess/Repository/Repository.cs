using Microsoft.EntityFrameworkCore;
using SFC_DataAccess.Data;
using SFC_DataAccess.Repository.Contracts;
using System.Linq.Expressions;

namespace SFC_DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SFCDBContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(SFCDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id).AsTask();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(List<T> entities)
        {
            if (entities != null && entities.Any())
            {
                _dbSet.AddRange(entities);
            }            
        }

        public void Remove(int id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }

            return query.FirstOrDefaultAsync();
        }

    }
}
