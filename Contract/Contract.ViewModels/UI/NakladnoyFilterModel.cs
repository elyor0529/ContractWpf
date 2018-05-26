using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.ViewModels.UI.Reports;

namespace Contract.ViewModels.UI
{
    public class NakladnoyFilterModel : ContractReportModel
    {
        public DateTime? NakladnoyDate { get; set; }

        public string NakladnoyNumber { get; set; }

        public double? Col13 { get; set; }

        public double? Col14_16 { get; set; }

        public double? Col15 { get; set; }

        public double? Col17_18 { get; set; }
         
        public double? Col19_24 { get; set; }

    }
}
