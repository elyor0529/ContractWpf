using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Contract.Data
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        int Count(Expression<Func<T, bool>> predicate);
        int Max(Func<T, int> predicate);
        int Count();
        int CountRaw(string query);
        IQueryable<T> GetAll();
        void Store(object value);
        void Store(T value);
        int SaveChanges();
        void Delete(object value);
        void Delete(T value);
        T GetById(object id);
        ICollection<T> RawQuery(string query);
        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
    }
}