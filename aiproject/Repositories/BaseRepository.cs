using System.Collections.Generic;
using System.Linq;
using aiproject.Entities;
using Microsoft.EntityFrameworkCore;

namespace aiproject.Repositories
{
    public abstract class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        protected readonly TContext Context;

        protected BaseRepository(TContext context)
        {
            Context = context;
        }

        public TEntity Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
            return entity;
        }

        public TEntity Delete(int id)
        {
            var entity = Context.Set<TEntity>().Find(id);
            if (entity == null)
            {
                return null;
            }

            Context.Set<TEntity>().Remove(entity);
            Context.SaveChanges();

            return entity;
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public List<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public TEntity Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChangesAsync();
            return entity;
        }
    }
}