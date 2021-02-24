using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PaymentProcessor.Domain.Interfaces
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        TEntity GetById(TKey id);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        TEntity Add(TEntity entity);

        bool AddRange(IEnumerable<TEntity> entities);

        bool Remove(TEntity entity);

        bool RemoveById(TKey id);

        TEntity Update(TEntity entity);

        bool RemoveRange(IEnumerable<TEntity> entities);

        bool Exists(TKey id);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
    }
}
