using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using ContosoUniversity.Models;
using System.Linq.Expressions;

namespace ContosoUniversity.DAL
{
    // TODO: Extract interface IGenericRepository
    public class GenericRepository<TEntity> where TEntity : class
    {
        private SchoolContext context;
        private DbSet<TEntity> dbSet;
        private Expression<Func<TEntity, object>>[] includeProperties;

        public GenericRepository(SchoolContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
             IQueryable<TEntity> query = dbSet;

            if (includeProperties != null)
            {
                query = includeProperties.Aggregate(query, (current, include) => current.Include(include));
                includeProperties = null;
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return orderBy == null ? query : orderBy(query);
        }

        public GenericRepository<TEntity> IncludeProperties(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            this.includeProperties = includeProperties;
            return this;
        }
        
        public virtual TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
