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
    public class WorkTypeService : IWorkTypeService
    {
        private static readonly Func<WorkTypeViewModel, WorkType, WorkType> ToDomain = (x, y) =>
        {
            if (y == null)
            {
                y = new WorkType();
            }
            y.Id = x.Id;
            y.Name = x.Name;
            y.Description = x.Descrption;
            y.Code = x.Code;

            return y;
        };

        private static Func<WorkType, WorkTypeViewModel> ToViewModel
        {
            get
            {
                return x =>
                {
                    return x == null ? new WorkTypeViewModel() : new WorkTypeViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Descrption = x.Description,
                        Code = x.Code
                    };
                };
            }
        }

        public IRepository<WorkType> Repository
        {
            get; private set;
        }

        public WorkTypeService()
        {
            var dbContext = DiConfig.Resolve<RepositoryContextBase>();

            Repository = dbContext.GetRepository<WorkType>();
        }

        public IList<WorkTypeViewModel> GetAll()
        {
            var result = Repository.GetAll()
                .AsEnumerable()
                .Select(x => ToViewModel(x))
                .ToList();

            return result;
        }

        public int CreateOrUpdate(WorkTypeViewModel item)
        {
            var data = (WorkType)null;

            if (item.Id > 0)
            {
                data = Repository.FirstOrDefault(x => x.Id == item.Id);
            }

            data = ToDomain(item, data);
            Repository.Store(data);
            Repository.SaveChanges();

            return data.Id;
        }

        public WorkTypeViewModel GetById(int id)
        {
            var data = Repository.FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found ");
            }

            return ToViewModel(data);
        }

        public WorkTypeViewModel Get(Expression<Func<WorkType, bool>> predicate)
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

        public void CreateOrUpdateByRange(IList<WorkTypeViewModel> t)
        {
            throw new NotImplementedException();
        }

        public IList<WorkTypeViewModel> GetAll(Expression<Func<WorkType, bool>> predicate)
        {
            var result = Repository.GetAll()
                    .Where(predicate)
                    .Select(x => ToViewModel(x))
                    .ToList();

            return result;
        }
         
        public int Max(Func<WorkType, int> selector)
        {
            throw new NotImplementedException();
        }
    }
}