using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VNH.BE.Domain.SeedWorks;

namespace VNH.BE.Infrastructure.Repositories
{
    public class GenericRepository<T, TKey> : IRepository<T, TKey> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<T> _dbSet;
        private Type _type;
        public IUnitOfWork UnitOfWork => _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _type = typeof(T);
            _dbSet = _dbContext.Set<T>();
        }

        public T Add(T entity)
        {
            SetInsert(entity);
            return _dbSet.Add(entity).Entity;
        }

        public T Update(T entity)
        {
            SetUpdate(entity);
            return _dbSet.Update(entity).Entity;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            foreach(var item in entities)
            {
                SetUpdate(item);
            }
            _dbSet.UpdateRange(entities);
        }

        public T GetById(TKey key)
        {
            return _dbSet.Find(key);
        }

        public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public void Delete(TKey key)
        {
            var entity = _dbSet.Find(key);
            _dbSet.Remove(entity);
        }

        public List<T> GetListBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        private bool HasProperty(string property)
        {
            return _type.GetProperty(property) != null;
        }

        private void SetInsert(T entity)
        {
            if(HasProperty("CreatedAt"))
            {
                _type.GetProperty("CreatedAt").SetValue(entity, DateTime.UtcNow);
            }

            if(HasProperty("CreatedBy"))
            {
                // Todo: Get current User
                _type.GetProperty("CreatedBy").SetValue(entity, "Admin");
            }
        }

        private void SetUpdate(T entity)
        {
            if (HasProperty("UpdatedAt"))
            {
                _type.GetProperty("UpdatedAt").SetValue(entity, DateTime.UtcNow);
            }

            if (HasProperty("UpdateBy"))
            {
                // Todo: Get current User
                _type.GetProperty("UpdateBy").SetValue(entity, "Admin");
            }
        }
    }
}
