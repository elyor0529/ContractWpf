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
    public class ContractDepartmentService : IContractDepartmentService
    {
        private static readonly Func<ContractDepartmentViewModel, ContractDepartment, ContractDepartment> ToDomain = (x, y) =>
        {
            if (y == null)
            {
                y = new ContractDepartment();
            }
            y.Id = x.Id;
            y.ContractId = x.ContractId;
            y.DepartmentId = x.DepartmentId;
            y.Date = x.Date;

            return y;
        };

        private static Func<ContractDepartment, ContractDepartmentViewModel> ToViewModel
        {
            get
            {
                return x =>
                {
                    return x == null ? new ContractDepartmentViewModel() : new ContractDepartmentViewModel
                    {
                        Id = x.Id,
                        ContractId = x.ContractId,
                        DepartmentId = x.DepartmentId,
                        Date = x.Date
                    };
                };
            }
        }

        public IRepository<ContractDepartment> Repository
        {
            get; private set;
        }

        public ContractDepartmentService()
        {
            var dbContext = DiConfig.Resolve<RepositoryContextBase>();

            Repository = dbContext.GetRepository<ContractDepartment>();
        }

        public IList<ContractDepartmentViewModel> GetAll()
        {
            var result = Repository.GetAll()
              .AsEnumerable()
              .Select(x => ToViewModel(x))
              .ToList();

            return result;
        }

        public IList<ContractDepartmentViewModel> GetByContract(int contractId)
        {
            var result = Repository.Where(x => x.ContractId == contractId)
             .AsEnumerable()
             .Select(x => ToViewModel(x))
             .ToList();

            return result;
        }

        public IList<ContractDepartmentViewModel> GetByDepartment(int departmentId)
        {
            var result = Repository.Where(x => x.DepartmentId == departmentId)
             .AsEnumerable()
             .Select(x => ToViewModel(x))
             .ToList();

            return result;
        }

        public int CreateOrUpdate(ContractDepartmentViewModel item)
        {
            var data = (ContractDepartment)null;

            if (item.Id > 0)
            {
                data = Repository.FirstOrDefault(x => x.Id == item.Id);
            }

            data = ToDomain(item, data);
            Repository.Store(data);
            Repository.SaveChanges();

            return data.Id;
        }

        public ContractDepartmentViewModel GetById(int id)
        {
            var data = Repository.FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found ");
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

        public ContractDepartmentViewModel Get(Expression<Func<ContractDepartment, bool>> predicate)
        {
            var data = Repository.FirstOrDefault(predicate);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found ");

                return null;
            }

            return ToViewModel(data);
        }

        public void CreateOrUpdateByRange(IList<ContractDepartmentViewModel> t)
        {
            throw new NotImplementedException();
        }

        public IList<ContractDepartmentViewModel> GetAll(Expression<Func<ContractDepartment, bool>> predicate)
        {
            var result = Repository.Where(predicate)
                    .AsEnumerable()
                    .Select(x => ToViewModel(x))
                    .ToList();

            return result;
        }


        public int Max(Func<ContractDepartment, int> selector)
        {
            throw new NotImplementedException();
        }
    }
}