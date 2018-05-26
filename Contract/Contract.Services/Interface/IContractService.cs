using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Contract.ViewModels;
using Contract.ViewModels.DAL;
using Contract.ViewModels.UI;
using Contract.ViewModels.UI.Reports;
using System.Linq;
using PagedList;

namespace Contract.Services.Interface
{
    public interface IContractService : IService<ContractViewModel, Data.Models.Contract>
    {

        void DoFilter(ref IQueryable<Data.Models.Contract> contracts, ContractFilterModel model);

        IList<ContractReportModel> Filter(ContractFilterModel model);

        IPagedList<ContractReportModel> Paging(ContractFilterModel model, int pageIndex, int pageSize);

        IList<ContractReportSection1ReportModel> Report1Filter(ContractFilterModel model);

        IList<ContractReportSection2ReportModel> Report2Filter(ContractFilterModel model);

        IList<ContractReportSection3ReportModel> Report3Filter(ContractFilterModel model);

        IList<ContractReportSection4ReportModel> Report4Filter(ContractFilterModel model);

        IList<ContractReportSection5ReportModel> Report5Filter(ContractFilterModel model);

        IList<ActInvoiceFilterModel> ActInvoiceFilterModels(ContractFilterModel model);

        IList<NakladnoyFilterModel> NakladnoyFilterModels(ContractFilterModel model);
    }
}