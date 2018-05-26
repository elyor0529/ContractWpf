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
    public class DepartmentService : IDepartmentService
    {

        private static readonly Func<DepartmentViewModel, Department, Department> ToDomain = (x, y) =>
        {
            if (y == null)
            {
                y = new Department();
            }
            y.Id = x.Id;
            y.Name = x.Name;
            y.Description = x.Description;

            return y;
        };

        private static Func<Department, DepartmentViewModel> ToViewModel
        {
            get
            {
                return x =>
                {
                    return x == null ? new DepartmentViewModel() : new DepartmentViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    };
                };
            }
        }

        public IRepository<Department> Repository
        {
            get; private set;
        }

        public DepartmentService()
        {
            var dbContext = DiConfig.Resolve<RepositoryContextBase>();

            Repository = dbContext.GetRepository<Department>();
        }

        public IList<DepartmentViewModel> GetAll()
        {
            var result = Repository.GetAll()
           .AsEnumerable()
           .Select(x => ToViewModel(x))
           .ToList();

            return result;
        }

        public int CreateOrUpdate(DepartmentViewModel item)
        {
            var data = (Department)null;

            if (item.Id > 0)
            {
                data = Repository.FirstOrDefault(x => x.Id == item.Id);
            }

            data = ToDomain(item, data);
            Repository.Store(data);
            Repository.SaveChanges();

            return data.Id;
        }

        public DepartmentViewModel GetById(int id)
        {
            var data = Repository.FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found ");
            }

            return ToViewModel(data);
        }

        public DepartmentViewModel Get(Expression<Func<Department, bool>> predicate)
        {
            var data = Repository.FirstOrDefault(predicate);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found this  Department");

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

        public void CreateOrUpdateByRange(IList<DepartmentViewModel> t)
        {
            throw new NotImplementedException();
        }

        public IList<DepartmentViewModel> GetAll(Expression<Func<Department, bool>> predicate)
        {
            var result = Repository.Where(predicate)
                    .AsEnumerable()
                    .Select(x => ToViewModel(x))
                    .ToList();

            return result;
        }
         
        public int Max(Func<Department, int> selector)
        {
            throw new NotImplementedException();
        }
    }
}