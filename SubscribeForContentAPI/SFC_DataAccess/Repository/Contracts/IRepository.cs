using SFC_DataEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SFC_DataAccess.Repository.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        void Add(T entity);
        void AddRange(List<T> entities);
        Task<bool> SaveChangesAsync();
        Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate);
        void Remove(int id);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null);
    }
}
