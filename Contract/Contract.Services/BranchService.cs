using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Contract.Core;
using Contract.Core.Helpers;
using Contract.Data;
using Contract.Data.Models;
using Contract.Services.Interface;
using Contract.ViewModels;
using Contract.ViewModels.DAL;

namespace Contract.Services
{
    public class BranchService : IBranchService
    {
        private static readonly Func<BranchViewModel, Branch, Branch> ToDomain = (x, y) =>
        {
            if (y == null)
            {
                y = new Branch();
            }
            y.Id = x.Id;
            y.Code = x.Code;
            y.Title = x.Title;

            return y;
        };

        private static Func<Branch, BranchViewModel> ToViewModel
        {
            get
            {
                return x =>
                {
                    return x == null ? new BranchViewModel() : new BranchViewModel
                    {
                        Id = x.Id,
                        Code = x.Code,
                        Title = x.Title
                    };
                };
            }
        }

        public IRepository<Branch> Repository
        {
            get;
            private set;
        }

        public BranchService()
        {
            var dbContext = DiConfig.Resolve<RepositoryContextBase>();

            Repository = dbContext.GetRepository<Branch>();
        }

        public IList<BranchViewModel> GetAll()
        {
            var result = Repository.GetAll()
                .AsEnumerable()
                .Select(x => ToViewModel(x))
                .ToList();

            return result;
        }

        public int CreateOrUpdate(BranchViewModel item)
        {
            var data = (Branch)null;

            if (item.Id > 0)
            {
                data = Repository.FirstOrDefault(x => x.Id == item.Id);
            }

            data = ToDomain(item, data);
            Repository.Store(data);
            Repository.SaveChanges();

            return data.Id;
        }

        public BranchViewModel GetById(int id)
        {
            var data = Repository.FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found ");
            }

            return ToViewModel(data);
        }

        public BranchViewModel Get(Expression<Func<Branch, bool>> predicate)
        {
            var data = Repository.FirstOrDefault(predicate);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found this  WorkType");

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

        public void CreateOrUpdateByRange(IList<BranchViewModel> t)
        {
            throw new NotImplementedException();
        }

        public IList<BranchViewModel> GetAll(Expression<Func<Branch, bool>> predicate)
        {
            var result = Repository.Where(predicate)
                    .AsEnumerable()
                    .Select(x => ToViewModel(x))
                    .ToList();

            return result;
        }

        public int Max(Func<Branch, int> selector)
        {
            return Repository.Max(selector);
        }
    }
}