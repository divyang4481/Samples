using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using ContosoUniversity.Models;
using System.Linq.Expressions;

namespace ContosoUniversity.DAL
{
    // TODO: Move to GenericRepository
    public static class GenericRepositoryExtensions
    {
        public static IQueryable<TEntity> IncludeProperties<TEntity>(this IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includeProperties)
            where TEntity: class
        {
            if (includeProperties != null)
            {
                query = includeProperties.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }
    }
}