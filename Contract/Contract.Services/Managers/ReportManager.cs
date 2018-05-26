using System.Collections.Generic;
using Contract.Core;
using Contract.Services.Interface;
using Contract.ViewModels.UI;
using Contract.ViewModels.UI.Reports;

namespace Contract.Services.Managers
{
    public  sealed class ReportManager
    {
      
        public IList<ContractReportSection1ReportModel> GetSectionReport1Models(ContractFilterModel model)
        {
            var contractService = DiConfig.Resolve<IContractService>();
            var contractModels = contractService.Report1Filter(model);

            return contractModels;
        }
         
        public IList<ContractReportSection2ReportModel> GetSectionReport2Models(ContractFilterModel model)
        {
            var contractService = DiConfig.Resolve<IContractService>();
            var contractModels = contractService.Report2Filter(model);

            return contractModels;
        }

        public IList<ContractReportSection3ReportModel> GetSectionReport3Models(ContractFilterModel model)
        {
            var contractService = DiConfig.Resolve<IContractService>();
            var contractModels = contractService.Report3Filter(model);

            return contractModels;
        }

        public IList<ContractReportSection4ReportModel> GetSectionReport4Models(ContractFilterModel model)
        {
            var contractService = DiConfig.Resolve<IContractService>();
            var contractModels = contractService.Report4Filter(model);

            return contractModels;
        }

        public IList<ContractReportSection5ReportModel> GetSectionReport5Models(ContractFilterModel model)
        {
            var contractService = DiConfig.Resolve<IContractService>();
            var contractModels = contractService.Report5Filter(model);

            return contractModels;
        }

    }
}
