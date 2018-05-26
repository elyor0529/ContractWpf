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
    public class ContractWorkPaymentService : IContractWorkPaymentService
    {

        private static readonly Func<ContractWorkPaymentViewModel, ContractWorkPayment, ContractWorkPayment> ToDomain = (x, y) =>
        {
            if (y == null)
            {
                y = new ContractWorkPayment();
            }
            y.Id = x.Id;
            y.ContractId = x.ContractId;
            y.WorkTypeId = x.WorkTypeId;
            y.Amount = x.Amount;
            y.PeriodId = x.PeriodId;

            return y;
        };

        private static Func<ContractWorkPayment, ContractWorkPaymentViewModel> ToViewModel
        {
            get
            {
                return x =>
                {
                    return x == null ? new ContractWorkPaymentViewModel() : new ContractWorkPaymentViewModel
                    {
                        Id = x.Id,
                        ContractId = x.ContractId,
                        WorkTypeId = x.WorkTypeId,
                        Amount = x.Amount,
                        PeriodId = x.PeriodId
                    };
                };
            }
        }

        public IRepository<ContractWorkPayment> Repository
        {
            get; private set;
        }

        public ContractWorkPaymentService()
        {
            var dbContext = DiConfig.Resolve<RepositoryContextBase>();

            Repository = dbContext.GetRepository<ContractWorkPayment>();
        }

        public IList<ContractWorkPaymentViewModel> GetAll()
        {
            var result = Repository.GetAll()
            .AsEnumerable()
            .Select(x => ToViewModel(x))
            .ToList();

            return result;
        }

        public IList<ContractWorkPaymentViewModel> GetByContract(int contractId)
        {
            var result = Repository.Where(x => x.ContractId == contractId)
                .AsEnumerable()
                .Select(x => ToViewModel(x))
                .ToList();

            return result;
        }

        public IList<ContractWorkPaymentViewModel> GetByWorkType(int worktypeId)
        {
            var result = Repository.Where(x => x.WorkTypeId == worktypeId)
              .AsEnumerable()
              .Select(x => ToViewModel(x))
              .ToList();

            return result;
        }

        public int CreateOrUpdate(ContractWorkPaymentViewModel item)
        {
            var data = (ContractWorkPayment)null;

            if (item.Id > 0)
            {
                data = Repository.FirstOrDefault(x => x.Id == item.Id);
            }

            data = ToDomain(item, data);
            Repository.Store(data);
            Repository.SaveChanges();

            return data.Id;
        }

        public ContractWorkPaymentViewModel GetById(int id)
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

        public ContractWorkPaymentViewModel Get(Expression<Func<ContractWorkPayment, bool>> predicate)
        {
            var data = Repository.FirstOrDefault(predicate);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found ");

                return null;
            }

            return ToViewModel(data);
        }

        public void CreateOrUpdateByRange(IList<ContractWorkPaymentViewModel> t)
        {
            throw new NotImplementedException();
        }

        public IList<ContractWorkPaymentViewModel> GetAll(Expression<Func<ContractWorkPayment, bool>> predicate)
        {
            var result = Repository.Where(predicate)
                    .AsEnumerable()
                    .Select(x => ToViewModel(x))
                    .ToList();

            return result;
        }


        public int Max(Func<ContractWorkPayment, int> selector)
        {
            throw new NotImplementedException();
        }
    }
}