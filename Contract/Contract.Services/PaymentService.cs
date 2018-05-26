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
    public class PaymentService : IPaymentService
    {
        private static readonly Func<PaymentViewModel, Payment, Payment> ToDomain = (x, y) =>
        {
            if (y == null)
            {
                y = new Payment();
            }
            y.Id = x.Id;
            y.Amount = x.Amount;
            y.Description = x.Description;
            y.ContractId = x.ContractId;

            return y;
        };

        private static Func<Payment, PaymentViewModel> ToViewModel
        {
            get
            {
                return x =>
                {
                    return x == null ? new PaymentViewModel() : new PaymentViewModel
                    {
                        Id = x.Id,
                        Date = x.Date,
                        Amount = x.Amount,
                        Description = x.Description,
                        ContractId = x.ContractId
                    };
                };
            }
        }

        public IRepository<Payment> Repository
        {
            get; private set;
        }

        public PaymentService()
        {
            var dbContext = DiConfig.Resolve<RepositoryContextBase>();

            Repository = dbContext.GetRepository<Payment>();
        }

        public IList<PaymentViewModel> GetAll()
        {
            var result = Repository.GetAll()
                .AsEnumerable()
                .Select(x => ToViewModel(x))
                .ToList();

            return result;
        }

        public List<PaymentViewModel> GetByContract(int contractId)
        {
            var result = Repository.Where(x => x.ContractId == contractId)
           .AsEnumerable()
           .Select(x => ToViewModel(x))
           .ToList();

            return result;
        }

        public int CreateOrUpdate(PaymentViewModel item)
        {
            var data = (Payment)null;

            if (item.Id > 0)
            {
                data = Repository.FirstOrDefault(x => x.Id == item.Id);
            }

            data = ToDomain(item, data);
            Repository.Store(data);
            Repository.SaveChanges();

            return data.Id;
        }

        public PaymentViewModel GetById(int id)
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

        public void CreateOrUpdateByRange(IList<PaymentViewModel> t)
        {
            throw new NotImplementedException();
        }

        public PaymentViewModel Get(Expression<Func<Payment, bool>> predicate)
        {
            var data = Repository.FirstOrDefault(predicate);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found this client");

                return null;
            }

            return ToViewModel(data);
        }

        public IList<PaymentViewModel> GetAll(Expression<Func<Payment, bool>> predicate)
        {
            var result = Repository.Where(predicate)
                     .AsEnumerable()
                     .Select(x => ToViewModel(x))
                     .ToList();

            return result;
        }


        public int Max(Func<Payment, int> selector)
        {
            throw new NotImplementedException();
        }
    }
}