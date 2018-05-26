using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Contract.Core;
using Contract.Core.Helpers;

namespace Contract.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            Guard.NotNull(context, "context");
            _context = context;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Count(predicate);
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int CountRaw(string query)
        {
            throw ErrorHelper.NotSupported();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public void Store(object value)
        {
            Guard.NotNull(value, "value");
            if (_context.Entry(value).State == EntityState.Detached)
            {
                var type = value.GetType();
                _context.Set(type).Add(value);
            }
        }

        public void Store(T value)
        {
            Guard.NotNull(value, "value");
            if (_context.Entry(value).State == EntityState.Detached)
                _context.Set<T>().Add(value);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Delete(object value)
        {
            Guard.NotNull(value, "value");
            if (_context.Entry(value).State != EntityState.Deleted)
                _context.Set(value.GetType()).Remove(value);
        }

        public void Delete(T value)
        {
            Guard.NotNull(value, "value");
            if (_context.Entry(value).State != EntityState.Deleted)
                _context.Set<T>().Remove(value);
        }

        public T GetById(object id)
        {
            throw ErrorHelper.NotSupported("This operation is not supported in this implementation of the repository");
        }

        public ICollection<T> RawQuery(string query)
        {
            Guard.NotNullOrEmpty(query, "query");
            return _context.Set<T>().SqlQuery(query).ToList();
        }


        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }


        public int Max(Func<T, int> predicate)
        {
            IQueryable<T> query = _context.Set<T>();

            return query.DefaultIfEmpty(default(T)).Max(predicate);
        }
    }
}