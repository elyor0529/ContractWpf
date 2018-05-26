using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.ViewModels.UI.Reports
{
    public class ContractReportSection4ReportModel : ContractReportModel
    {
        public double? Paid { get; set; }
         
        public DateTime? Col27 { get; set; }
        public DateTime? Col28 { get; set; }
        public DateTime? Col31 { get; set; }
    }
}