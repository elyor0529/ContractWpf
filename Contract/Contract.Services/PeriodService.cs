using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contract.Core;
using Contract.Core.Helpers;
using Contract.Data;
using Contract.Data.Models;
using Contract.Services.Interface;
using Contract.ViewModels.DAL;

namespace Contract.Services
{
    public class PeriodService : IPeriodService
    {
        private static readonly Func<PeriodViewModel, Period, Period> ToDomain = (x, y) =>
       {
           if (y == null)
           {
               y = new Period();
           }
           y.Id = x.Id;
           y.Title = x.Title;
           y.Description = x.Description;

           return y;
       };

        private static Func<Period, PeriodViewModel> ToViewModel
        {
            get
            {
                return x =>
                {
                    return x == null ? new PeriodViewModel() : new PeriodViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description
                    };
                };
            }
        }

        public IRepository<Period> Repository
        {
            get; private set;
        }

        public PeriodService()
        {
            var dbContext = DiConfig.Resolve<RepositoryContextBase>();

            Repository = dbContext.GetRepository<Period>();
        }

        public IList<PeriodViewModel> GetAll()
        {
            var result = Repository.GetAll()
                .AsEnumerable()
                .Select(x => ToViewModel(x))
                .ToList();

            return result;
        }

        public int CreateOrUpdate(PeriodViewModel item)
        {
            var data = (Period)null;

            if (item.Id > 0)
            {
                data = Repository.FirstOrDefault(x => x.Id == item.Id);
            }

            data = ToDomain(item, data);
            Repository.Store(data);
            Repository.SaveChanges();

            return data.Id;
        }

        public PeriodViewModel GetById(int id)
        {
            var data = Repository.FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found ");
            }

            return ToViewModel(data);
        }

        public PeriodViewModel Get(Expression<Func<Period, bool>> predicate)
        {
            var data = Repository.FirstOrDefault(predicate);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found this  Period");

                return null;
            }

            return ToViewModel(data);
        }

        public bool Delete(int id)
        {
            var data = Repository.FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found");
            }
            Repository.Delete(data);

            return Repository.SaveChanges() > 0;
        }

        public void CreateOrUpdateByRange(IList<PeriodViewModel> t)
        {
            throw new NotImplementedException();
        }

        public IList<PeriodViewModel> GetAll(Expression<Func<Period, bool>> predicate)
        {
            var result = Repository.Where(predicate)
                    .AsEnumerable()
                    .Select(x => ToViewModel(x))
                    .ToList();

            return result;
        }

        public int Max(Func<Period, int> selector)
        {
            throw new NotImplementedException();
        }
    }
}
