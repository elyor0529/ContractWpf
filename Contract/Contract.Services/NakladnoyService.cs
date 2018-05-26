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
    public class NakladnoyService : INakladnoyService
    {

        private static readonly Func<NakladnoyViewModel, Nakladnoy, Nakladnoy> ToDomain = (x, y) =>
        {
            if (y == null)
            {
                y = new Nakladnoy();
            }
            y.Id = x.Id;
            y.Number = x.Number;
            y.Date = x.Date;
            y.ContractId = x.ContractId;

            return y;
        };

        private static Func<Nakladnoy, NakladnoyViewModel> ToViewModel
        {
            get
            {
                return x =>
                {
                    return x == null ? new NakladnoyViewModel() : new NakladnoyViewModel
                    {
                        Id = x.Id,
                        Number = x.Number,
                        Date = x.Date,
                        ContractId = x.ContractId
                    };
                };
            }
        }

        public IRepository<Nakladnoy> Repository
        {
            get; private set;
        }

        public NakladnoyService()
        {
            var dbContext = DiConfig.Resolve<RepositoryContextBase>();

            Repository = dbContext.GetRepository<Nakladnoy>();
        }

        public IList<NakladnoyViewModel> GetAll()
        {
            var result = Repository.GetAll()
             .AsEnumerable()
             .Select(x => ToViewModel(x))
             .ToList();

            return result;
        }

        public IList<NakladnoyViewModel> GetByContract(int contractId)
        {
            var result = Repository.Where(x => x.ContractId == contractId)
           .AsEnumerable()
           .Select(x => ToViewModel(x))
           .ToList();

            return result;
        }

        public int CreateOrUpdate(NakladnoyViewModel item)
        {
            var data = (Nakladnoy)null;

            if (item.Id > 0)
            {
                data = Repository.FirstOrDefault(x => x.Id == item.Id);
            }

            data = ToDomain(item, data);
            Repository.Store(data);
            Repository.SaveChanges();

            return data.Id;
        }

        public NakladnoyViewModel GetById(int id)
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

        public NakladnoyViewModel Get(Expression<Func<Nakladnoy, bool>> predicate)
        {
            var data = Repository.FirstOrDefault(predicate);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found ");

                return null;
            }

            return ToViewModel(data);
        }

        public void CreateOrUpdateByRange(IList<NakladnoyViewModel> t)
        {
            throw new NotImplementedException();
        }

        public IList<NakladnoyViewModel> GetAll(Expression<Func<Nakladnoy, bool>> predicate)
        {
            var result = Repository.Where(predicate)
                    .AsEnumerable()
                    .Select(x => ToViewModel(x))
                    .ToList();

            return result;
        } 

        public int Max(Func<Nakladnoy, int> selector)
        {
            throw new NotImplementedException();
        }
    }
}