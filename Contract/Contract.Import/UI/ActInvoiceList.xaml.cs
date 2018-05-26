using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Contract.Core;
using Contract.Services.Helpers;
using Contract.Services.Interface;
using Contract.Services.Managers;
using Contract.ViewModels.UI;
using Contract.ViewModels.UI.Reports;
using UserControl = System.Windows.Controls.UserControl;

namespace Contract.Import.UI
{
    /// <summary>
    /// Interaction logic for ActInvoiceList.xaml
    /// </summary>
    public partial class ActInvoiceList : UserControl
    { 
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private ContractFilterModel _model;
        private readonly IContractService _contractService;

        public ActInvoiceList()
        {
            _contractService = DiConfig.Resolve<IContractService>();
             
            InitializeComponent();

            _worker.WorkerReportsProgress = true;
            _worker.DoWork += WorkerOnDoWork;
            _worker.ProgressChanged += WorkerOnProgressChanged;
            _worker.RunWorkerCompleted += WorkerOnRunWorkerCompleted;
        }

        private void WorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            if (runWorkerCompletedEventArgs.Cancelled || runWorkerCompletedEventArgs.Error != null)
                return;

            DataGrid.Items.Add(new ContractReportSection5ReportModel());

            var model = (ActInvoiceFilterModel)runWorkerCompletedEventArgs.Result;

            DataGrid.Items.Add(model);
        }


        private void WorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            var model = (ActInvoiceFilterModel)progressChangedEventArgs.UserState;

            DataGrid.Items.Add(model);
        }

        private void WorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var models = _contractService.ActInvoiceFilterModels(_model);


            foreach (var model in models)
            {
                _worker.ReportProgress(0, model);
            }

            doWorkEventArgs.Result = new ActInvoiceFilterModel
            {
                Object = "Итого",
                OwnPrice = models.Sum(s => s.OwnPrice),
                NDSPrice = models.Sum(s => s.NDSPrice)
            };
        }

        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            if (_worker.IsBusy)
                return;

            _model = FilterControl.Model; 

            if (!DataGrid.Items.IsEmpty)
                DataGrid.Items.Clear();

            _worker.RunWorkerAsync();
        }

        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            if (_model == null || DataGrid.Items.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show(@"Please enter to filter fields", @"Filter", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var filePath = "";

                Process.Start(filePath);
            }
            catch (Exception exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void DataGrid_OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

    }
}
