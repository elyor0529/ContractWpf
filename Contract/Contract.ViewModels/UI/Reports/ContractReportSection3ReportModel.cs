using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.ViewModels.UI.Reports
{
    public enum ContractStatus
    { 
        Yes,
        No
    }

    public class ContractReportSection3ReportModel : ContractReportModel
    {

        public double? Paid { get; set; }

        public double? Debt { get; set; }

        public ContractStatus? ContractStatus { get; set; }

        public string ActInvoiceNumber { get; set; }

    }
}
