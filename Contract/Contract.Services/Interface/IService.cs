using Contract.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Services.Interface
{
    public interface IService<TModel, TEnt> where TEnt : class
    {
        IRepository<TEnt> Repository { get; }

        IList<TModel> GetAll();
        int CreateOrUpdate(TModel t);
        void CreateOrUpdateByRange(IList<TModel> t);
        TModel GetById(int id);
        bool Delete(int id);

        TModel Get(Expression<Func<TEnt, bool>> predicate);
        IList<TModel> GetAll(Expression<Func<TEnt, bool>> predicate);
        int Max(Func<TEnt, int> selector);

    }
}
