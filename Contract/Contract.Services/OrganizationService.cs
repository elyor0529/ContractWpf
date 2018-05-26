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
    public class OrganizationService : IOrganizationService
    {
        private static readonly Func<OrganizationViewModel, Organization, Organization> ToDomain = (x, y) =>
        {
            if (y == null)
            {
                y = new Organization();
            }
            y.Id = x.Id;
            y.Name = x.Name;
            y.Inn = x.Inn;
            y.AccountNumber = x.AccountNumber;
            y.Okohx = x.Okohx;
            y.PhoneNumbers = x.PhoneNumbers;
            y.BankName1 = x.BankName1;
            y.Mfo1 = x.Mfo1;
            y.BankName2 = x.BankName2;
            y.Mfo2 = x.Mfo2;
            y.BankName3 = x.BankName3;
            y.Mfo3 = x.Mfo3;
            y.Chief = x.Chief;
            y.Foundation = x.Foundation;
            y.KS = x.KS;
            y.LS = x.LS;
            y.LegalAddress = x.LegalAddress;
            y.Position = x.Position;
            y.Postcode = x.Postcode;
            y.TypeOwnership = x.TypeOwnership;

            return y;
        };

        private static Func<Organization, OrganizationViewModel> ToViewModel
        {
            get
            {
                return x =>
                {
                    return x == null ? new OrganizationViewModel() : new OrganizationViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Inn = x.Inn,
                        AccountNumber = x.AccountNumber,
                        Okohx = x.Okohx,
                        PhoneNumbers = x.PhoneNumbers,
                        BankName1 = x.BankName1,
                        Mfo1 = x.Mfo1,
                        BankName2 = x.BankName2,
                        Mfo2 = x.Mfo2,
                        BankName3 = x.BankName3,
                        Mfo3 = x.Mfo3,
                        Chief = x.Chief,
                        Foundation = x.Foundation,
                        KS = x.KS,
                        LS = x.LS,
                        Postcode = x.Postcode,
                        TypeOwnership = x.TypeOwnership,
                        Position = x.Position,
                        LegalAddress = x.LegalAddress
                    };
                };
            }
        }

        public IRepository<Organization> Repository
        {
            get;
            private set;
        }

        public OrganizationService()
        {
            var dbContext = DiConfig.Resolve<RepositoryContextBase>();

            Repository = dbContext.GetRepository<Organization>();
        }

        public IList<OrganizationViewModel> GetAll()
        {
            var result = Repository.GetAll()
                .AsEnumerable()
                .Select(x => ToViewModel(x))
                .ToList();

            return result;
        }

        public int CreateOrUpdate(OrganizationViewModel item)
        {
            var data = (Organization)null;

            if (item.Id > 0)
            {
                data = Repository.FirstOrDefault(x => x.Id == item.Id);
            }

            data = ToDomain(item, data);
            Repository.Store(data);
            Repository.SaveChanges();

            return data.Id;
        }

        public OrganizationViewModel GetById(int id)
        {
            var data = Repository.FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found ");
            }

            return ToViewModel(data);
        }

        public OrganizationViewModel Get(Expression<Func<Organization, bool>> predicate)
        {
            var data = Repository.FirstOrDefault(predicate);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found this client");

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

        public void CreateOrUpdateByRange(IList<OrganizationViewModel> t)
        {
            throw new NotImplementedException();
        }

        public IList<OrganizationViewModel> GetAll(Expression<Func<Organization, bool>> predicate)
        { 
            var result = Repository.Where(predicate)
                    .AsEnumerable()
                    .Select(x => ToViewModel(x))
                    .ToList();

            return result;
        }


        public int Max(Func<Organization, int> selector)
        {
            throw new NotImplementedException();
        }
    }
}