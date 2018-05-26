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
    public class ActInvoiceService : IActInvoiceService
    {
        private static readonly Func<ActInvoiceViewModel, ActInvoice, ActInvoice> ToDomain = (x, y) =>
        {
            if (y == null)
            {
                y = new ActInvoice();
            }
            y.Id = x.Id;
            y.Number = x.Number;
            y.Description = x.Description;
            y.ContractId = x.ContractId;

            return y;
        };

        private static Func<ActInvoice, ActInvoiceViewModel> ToViewModel
        {
            get
            {
                return x =>
                {
                    return x == null ? new ActInvoiceViewModel() : new ActInvoiceViewModel
                    {
                        Id = x.Id,
                        Number = x.Number,
                        Description = x.Description,
                        ContractId = x.ContractId
                    };
                };
            }
        }

        public IRepository<ActInvoice> Repository
        {
            get; private set;
        }

        public ActInvoiceService()
        {
            var dbContext = DiConfig.Resolve<RepositoryContextBase>();

            Repository = dbContext.GetRepository<ActInvoice>();
        }

        public IList<ActInvoiceViewModel> GetAll()
        {
            var result = Repository.GetAll()
                .AsEnumerable()
                .Select(x => ToViewModel(x))
                .ToList();

            return result;
        }

        public IList<ActInvoiceViewModel> GetByContract(int contractId)
        {
            var result = Repository.Where(x => x.ContractId == contractId)
                .AsEnumerable()
                .Select(x => ToViewModel(x))
                .ToList();

            return result;
        }

        public int CreateOrUpdate(ActInvoiceViewModel item)
        {
            var data = (ActInvoice)null;

            if (item.Id > 0)
            {
                data = Repository.FirstOrDefault(x => x.Id == item.Id);
            }

            data = ToDomain(item, data);
            Repository.Store(data);
            Repository.SaveChanges();

            return data.Id;
        }

        public ActInvoiceViewModel GetById(int id)
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

        public ActInvoiceViewModel Get(Expression<Func<ActInvoice, bool>> predicate)
        {
            var data = Repository.FirstOrDefault(predicate);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found ");

                return null;
            }

            return ToViewModel(data);
        }
         
        public void CreateOrUpdateByRange(IList<ActInvoiceViewModel> t)
        {
            throw new NotImplementedException();
        }

        public IList<ActInvoiceViewModel> GetAll(Expression<Func<ActInvoice, bool>> predicate)
        {
            var result = Repository.Where(predicate)
                   .AsEnumerable()
                   .Select(x => ToViewModel(x))
                   .ToList();

            return result;
        }

        public int Max(Func<ActInvoice, int> selector)
        {
            throw new NotImplementedException();
        }
    }
}