using System.Data.Entity;
using Contract.Core;

namespace Contract.Data
{
    public class SqlServerRepositoryContext : RepositoryContextBase
    {
        private readonly DbContext _context;
         
        public SqlServerRepositoryContext(DbContext context)
        {
            Guard.NotNull(context, "context");
            _context = context;
        }

        public override IRepository<T> GetRepository<T>()
        {
            return new Repository<T>(_context);
        }

    }
}