using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class RepositoryBase<TEntity, TContext> : IRepository<TEntity>
            where TContext : DbContext
        where TEntity : class
    {
        protected TContext Context { get; }

        public RepositoryBase(TContext context)
        {
            Context = context;
        }
        public TEntity Add(TEntity entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
            return entity;
        }


        public void Delete(TEntity entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            
                return Context.Set<TEntity>().SingleOrDefault(filter);
            
        }
        public void Update(TEntity entity)
        {
                var updatedEntity = Context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                Context.SaveChanges();
        }
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {

                return filter == null
                    ? Context.Set<TEntity>().ToList()
                    : Context.Set<TEntity>().Where(filter).ToList();
        }

        public bool CheckIfExists(Expression<Func<TEntity, bool>> filter)
        {
            return Context.Set<TEntity>().Any(filter);
        }
    }
}
