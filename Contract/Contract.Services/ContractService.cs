using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Contract.Core;
using Contract.Core.Extensions;
using Contract.Core.Helpers;
using Contract.Data;
using Contract.Data.Models;
using Contract.Data.Models.Enums;
using Contract.Services.Interface;
using Contract.ViewModels.DAL;
using Contract.ViewModels.UI;
using Contract.ViewModels.UI.Reports;
using PagedList;

namespace Contract.Services
{
    public class ContractService : IContractService
    {

        private static readonly Func<ContractViewModel, Data.Models.Contract, Data.Models.Contract> ToDomain = (x, y) =>
        {
            if (y == null)
            {
                y = new Data.Models.Contract();
            }
            y.Id = x.Id;
            y.ObjectName = x.ObjectName;
            y.ContractNumber = x.ContractNumber;
            y.ContractDate = x.ContractDate;
            y.ContractAmount = x.ContractAmount;
            y.DeadlinesContract = x.DeadlinesContract;
            y.DeadlinesFact = x.DeadlinesFact;
            y.Limitation = x.Limitation;
            y.OrganizationId = x.OrganizationId;
            y.BranchId = x.BranchId;
            y.IsTriguare = x.IsTriguare;
            y.CategoryId = x.CategoryId;

            return y;
        };

        private static Func<Data.Models.Contract, ContractViewModel> ToViewModel
        {
            get
            {
                return x =>
                {
                    return x == null ? new ContractViewModel() : new ContractViewModel
                    {
                        Id = x.Id,
                        ObjectName = x.ObjectName,
                        ContractNumber = x.ContractNumber,
                        ContractDate = x.ContractDate,
                        ContractAmount = x.ContractAmount,
                        DeadlinesContract = x.DeadlinesContract,
                        DeadlinesFact = x.DeadlinesFact,
                        Limitation = x.Limitation,
                        CategoryId = x.CategoryId,
                        OrganizationId = x.OrganizationId,
                        BranchId = x.BranchId,
                        IsTriguare = x.IsTriguare
                    };
                };
            }
        }

        public IRepository<Data.Models.Contract> Repository
        {
            get;
            private set;
        }

        public ContractService()
        {
            var dbContext = DiConfig.Resolve<RepositoryContextBase>();

            Repository = dbContext.GetRepository<Data.Models.Contract>();
        }

        public void DoFilter(ref IQueryable<Data.Models.Contract> contracts, ContractFilterModel model)
        {
            if (contracts == null)
                return;

            if (model == null)
                return;

            if (!String.IsNullOrWhiteSpace(model.Number))
                contracts = contracts.Where(w => w.ContractNumber.Contains(model.Number));

            if (!String.IsNullOrWhiteSpace(model.Nick))
                contracts = contracts.Where(w => w.ObjectName.Contains(model.Nick) ||
                                               w.Organization.Name.Contains(model.Nick));

            if (model.EndDate != null && model.StartDate != null && model.StartDate <= model.EndDate)
                contracts = contracts.Where(w => w.ContractDate >= model.StartDate && w.ContractDate <= model.EndDate);
        }

        public IList<ContractReportModel> Filter(ContractFilterModel model)
        {
            var conracts = Repository.Include(a => a.Organization);

            DoFilter(ref conracts, model);

            return conracts.Select(s => new ContractReportModel
            {
                Object = s.ObjectName,
                Date = s.ContractDate,
                Client = s.Organization.Name,
                Number = s.ContractNumber,
                Amount = s.ContractAmount,
                Id = s.Id
            }).OrderByDescending(o => o.Id).ToList();
        }

        public IList<ContractViewModel> GetAll()
        {
            var result = Repository.GetAll()
               .AsEnumerable()
               .Select(x => ToViewModel(x))
               .ToList();

            return result;
        }

        public IList<ContractViewModel> GetByOrganization(int organizationId)
        {
            var result = Repository.Where(x => x.OrganizationId == organizationId)
               .AsEnumerable()
               .Select(x => ToViewModel(x))
               .ToList();

            return result;
        }

        public int CreateOrUpdate(ContractViewModel item)
        {
            var data = (Data.Models.Contract)null;

            if (item.Id > 0)
            {
                data = Repository.FirstOrDefault(x => x.Id == item.Id);
            }

            data = ToDomain(item, data);

            Repository.Store(data);
            Repository.SaveChanges();

            return data.Id;
        }

        public ContractViewModel GetById(int id)
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

        public ContractViewModel Get(Expression<Func<Data.Models.Contract, bool>> predicate)
        {
            var data = Repository.FirstOrDefault(predicate);

            if (data == null)
            {
                ErrorHelper.NotFound("Not found ");

                return null;
            }

            return ToViewModel(data);
        }

        public IList<ContractReportSection1ReportModel> Report1Filter(ContractFilterModel model)
        {
            var conracts = Repository.Where(w => w.ContractDate != null &&
                                                 w.ContractAmount > 0 &&
                                                 (w.ContractAmount - w.Payments.Sum(s => s.Amount)) > 0 &&
                                                 w.ActInvoices.Count > 0 &&
                                                 w.DeadlinesFact != null);

            DoFilter(ref conracts, model);

            return conracts.Select(s => new ContractReportSection1ReportModel
            {
                Object = s.ObjectName,
                Date = s.ContractDate,
                Client = s.Organization.Name,
                Number = s.ContractNumber,
                Amount = s.ContractAmount,
                Paid = s.Payments.Sum(payment => payment.Amount),
                Comment = s.Limitation,
                Debt = s.ContractAmount - s.Payments.Sum(payment => payment.Amount)
            }).ToList();
        }

        public IList<ContractReportSection2ReportModel> Report2Filter(ContractFilterModel model)
        {
            var conracts = Repository.Where(w => w.ContractDate != null &&
                                                 w.ContractAmount > 0 &&
                                                 w.DeadlinesContract == null);

            DoFilter(ref conracts, model);

            return conracts.Select(s => new ContractReportSection2ReportModel
            {
                Object = s.ObjectName,
                Date = s.ContractDate,
                Client = s.Organization.Name,
                Number = s.ContractNumber,
                Amount = s.ContractAmount,
                Paid = s.Payments.Sum(payment => payment.Amount),
                Residue = s.ContractAmount - s.Payments.Sum(payment => payment.Amount),
                Col13 = s.ContractWorkPayments.FirstOrDefault(f => f.WorkTypeId == (int?)WorkTypeCode.Col13).Amount,
                Col14 = s.ContractWorkPayments.FirstOrDefault(f => f.WorkTypeId == (int?)WorkTypeCode.Col14).Amount,
                Col15 = s.ContractWorkPayments.FirstOrDefault(f => f.WorkTypeId == (int?)WorkTypeCode.Col15).Amount,
                Col16 = s.ContractWorkPayments.FirstOrDefault(f => f.WorkTypeId == (int?)WorkTypeCode.Col16).Amount,
                Col17 = s.ContractWorkPayments.FirstOrDefault(f => f.WorkTypeId == (int?)WorkTypeCode.Col17).Amount,
                Col18 = s.ContractWorkPayments.FirstOrDefault(f => f.WorkTypeId == (int?)WorkTypeCode.Col18).Amount,
                Col19 = s.ContractWorkPayments.FirstOrDefault(f => f.WorkTypeId == (int?)WorkTypeCode.Col19).Amount,
                Col24 = s.ContractWorkPayments.FirstOrDefault(f => f.WorkTypeId == (int?)WorkTypeCode.Col24).Amount,
            }).ToList();
        }

        public IList<ContractReportSection3ReportModel> Report3Filter(ContractFilterModel model)
        {
            var conracts = Repository.Where(w => w.DeadlinesContract != null &&
                                                 (w.ContractDate == null || w.Payments.Count == 0 ||
                                                  w.ActInvoices.Count == 0));

            DoFilter(ref conracts, model);

            return conracts.Select(s => new ContractReportSection3ReportModel
            {
                Client = s.Organization.Name,
                Object = s.ObjectName,
                Number = s.ContractNumber,
                Date = s.ContractDate,
                Amount = s.ContractAmount,
                Paid = s.Payments.Sum(p => p.Amount),
                Debt = s.ContractAmount - s.Payments.Sum(p => p.Amount),
                ActInvoiceNumber = s.ActInvoices.FirstOrDefault().Number,
                ContractStatus = s.ContractDate != null ? ContractStatus.Yes : ContractStatus.No
            }).ToList();
        }

        public IList<ContractReportSection4ReportModel> Report4Filter(ContractFilterModel model)
        {
            var conracts = Repository.Where(w => (w.Payments.Sum(s => s.Amount) - w.ContractAmount) == 0 &&
                                          w.DeadlinesContract == null);

            DoFilter(ref conracts, model);

            return conracts.Select(s => new ContractReportSection4ReportModel
            {
                Client = s.Organization.Name,
                Object = s.ObjectName,
                Number = s.ContractNumber,
                Date = s.ContractDate,
                Amount = s.ContractAmount,
                Paid = s.Payments.Sum(p => p.Amount),
                Col27 = s.ContractDepartments.FirstOrDefault(f => f.DepartmentId == (int?)DepartmentCode.Col27).Date,
                Col28 = s.ContractDepartments.FirstOrDefault(f => f.DepartmentId == (int?)DepartmentCode.Col28).Date,
                Col31 = s.ContractDepartments.FirstOrDefault(f => f.DepartmentId == (int?)DepartmentCode.Col31).Date
            }).ToList();
        }

        public IList<ContractReportSection5ReportModel> Report5Filter(ContractFilterModel model)
        {
            var conracts = Repository.Where(w => (w.Payments.Sum(s => s.Amount) - w.ContractAmount) == 0 &&
                                                 w.DeadlinesFact != null);

            DoFilter(ref conracts, model);

            return conracts.Select(s => new ContractReportSection5ReportModel
            {
                Client = s.Organization.Name,
                Object = s.ObjectName,
                Number = s.ContractNumber,
                Date = s.ContractDate,
                Amount = s.ContractAmount
            }).ToList();
        }

        public IList<ActInvoiceFilterModel> ActInvoiceFilterModels(ContractFilterModel model)
        {
            var conracts = Repository.Include(a => a.Organization)
               .Where(w => w.ContractDate != null &&
                            w.ContractAmount > 0);

            DoFilter(ref conracts, model);

            return conracts.Select(s => new ActInvoiceFilterModel
            {
                Object = s.ObjectName,
                Date = s.ContractDate,
                Client = s.Organization.Name,
                Number = s.ContractNumber,
                Amount = s.ContractAmount
            }).ToList();
        }

        public IList<NakladnoyFilterModel> NakladnoyFilterModels(ContractFilterModel model)
        {
            var conracts = Repository.Include(a => a.Organization,
                a => a.Nakladnoys,
                a => a.ContractWorkPayments,
                a => a.ContractWorkPayments.Select(s => s.WorkType))
                .Where(w => w.ContractDate != null &&
                            w.ContractAmount > 0);

            DoFilter(ref conracts, model);

            return conracts.Select(s => new NakladnoyFilterModel
            {
                Object = s.ObjectName,
                Date = s.ContractDate,
                Client = s.Organization.Name,
                Number = s.ContractNumber,
                Amount = s.ContractAmount,
                NakladnoyDate = s.Nakladnoys.FirstOrDefault().Date,
                NakladnoyNumber = s.Nakladnoys.FirstOrDefault().Number,
                Col13 = s.ContractWorkPayments.Where(f => f.WorkType.Code == WorkTypeCode.Col13).Sum(a => a.Amount),
                Col14_16 = s.ContractWorkPayments.Where(f => f.WorkType.Code == WorkTypeCode.Col14 || f.WorkType.Code == WorkTypeCode.Col16).Sum(a => a.Amount),
                Col15 = s.ContractWorkPayments.Where(f => f.WorkType.Code == WorkTypeCode.Col15).Sum(a => a.Amount),
                Col17_18 = s.ContractWorkPayments.Where(f => f.WorkType.Code == WorkTypeCode.Col17 || f.WorkType.Code == WorkTypeCode.Col18).Sum(a => a.Amount),
                Col19_24 = s.ContractWorkPayments.Where(f => f.WorkType.Code >= WorkTypeCode.Col19 && f.WorkType.Code <= WorkTypeCode.Col24).Sum(a => a.Amount)
            }).ToList();
        }

        public void CreateOrUpdateByRange(IList<ContractViewModel> t)
        {
            throw new NotImplementedException();
        }

        public IList<ContractViewModel> GetAll(Expression<Func<Data.Models.Contract, bool>> predicate)
        {
            var result = Repository.Where(predicate)
                    .AsEnumerable()
                    .Select(x => ToViewModel(x))
                    .ToList();

            return result;
        }

        public int Max(Func<Data.Models.Contract, int> selector)
        {
            if (Repository.Count() > 0)
                return Repository.Max(selector);

            return 0;
        }

        public IPagedList<ContractReportModel> Paging(ContractFilterModel model, int pageIndex, int pageSize)
        {
            var conracts = Repository.Include(a => a.Organization);                   

            DoFilter(ref conracts, model);

            return conracts.Select(s => new ContractReportModel
            {
                Object = s.ObjectName,
                Date = s.ContractDate,
                Client = s.Organization.Name,
                Number = s.ContractNumber,
                Amount = s.ContractAmount,
                Id = s.Id
            }).OrderByDescending(o => o.Id).ToPagedList(pageIndex, pageSize);
        }
    }
}