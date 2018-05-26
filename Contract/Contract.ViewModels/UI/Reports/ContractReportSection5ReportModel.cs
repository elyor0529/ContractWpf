using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Contract.ViewModels.Annotations;

namespace Contract.ViewModels.UI.Reports
{
    public class ContractReportSection5ReportModel : ContractReportModel
    {
        private const double NDS_PERCENT = 17.6d;

        public double? OwnPrice { get; set; }

        public string OwnPriceStr
        {
            get { return String.Format("{0:N}", OwnPrice); }
        }

        private double? _ndsPrice;

        public double? NDSPrice
        {
            get { return _ndsPrice; }
            set
            {
                _ndsPrice = value;

                OwnPrice = _amount - _ndsPrice;

                OnPropertyChanged();
                OnPropertyChanged("OwnPrice");
            }
        }

        public string NDSPriceStr
        {
            get { return String.Format("{0:N}", NDSPrice); }
        }

        private double? _amount;

        public override double? Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;

                NDSPrice = _amount * NDS_PERCENT / 100;

                OnPropertyChanged();
                OnPropertyChanged("NDSPrice");
            }
        }


    }
}
