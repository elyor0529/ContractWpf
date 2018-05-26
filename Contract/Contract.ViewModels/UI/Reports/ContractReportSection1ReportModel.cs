using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.ViewModels.UI.Reports
{

    public class ContractReportSection1ReportModel : ContractReportModel
    {

        public double? Paid { get; set; }

        public double? Debt { get; set; }

        public string Comment { get; set; }

    }

}
