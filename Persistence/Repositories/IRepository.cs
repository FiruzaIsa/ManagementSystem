using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public interface IRepository<T>
    {
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Get(Expression<Func<T, bool>> filter);
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        bool CheckIfExists(Expression<Func<T, bool>> filter);
    }
}