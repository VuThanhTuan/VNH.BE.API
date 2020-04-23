using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace VNH.BE.Domain.SeedWorks
{
    public interface IRepository<T, Tkey> where T : class
    {
        IUnitOfWork UnitOfWork { get;}

        T Add(T entity);

        void AddRange(IEnumerable<T> entities);

        T Update(T entity);

        void UpdateRange(IEnumerable<T> entities);

        T GetById(Tkey key);

        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);

        List<T> GetListBy(Expression<Func<T, bool>> predicate);

        void Delete(Tkey key);

    }
}
