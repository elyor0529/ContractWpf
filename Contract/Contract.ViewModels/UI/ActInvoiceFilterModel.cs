using System;
using Contract.ViewModels.UI.Reports;

namespace Contract.ViewModels.UI
{
    public class ActInvoiceFilterModel : ContractReportModel
    {
        private const double NDS_PERCENT = 17.6d;

        public double? OwnPrice { get; set; }

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
