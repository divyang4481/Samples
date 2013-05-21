using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OSIM.Core.Persistence
{
    public interface IRepository<TEntity> where TEntity: class
    {
        IEnumerable<TEntity> FindAll();
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        TEntity FindById(int id);
        TEntity Add(TEntity newEntity);
        void Remove(TEntity entity);
    }
}
