using System.Windows.Controls;
using Contract.ViewModels.UI;

namespace Contract.Import.Tools
{
    /// <summary>
    /// Interaction logic for FilterControl.xaml
    /// </summary>
    public partial class FilterControl : UserControl
    {

        public FilterControl()
        {
            InitializeComponent();
        }

        public ContractFilterModel Model
        {
            get
            {
                return new ContractFilterModel
                {
                    Number = Number.Text,
                    Nick = Nick.Text,
                    EndDate = EndDatePicker.SelectedDate,
                    StartDate = StartDatePicker.SelectedDate
                };
            }
        }

    }
}
