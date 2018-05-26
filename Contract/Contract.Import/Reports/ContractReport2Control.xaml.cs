using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Contract.Services.Helpers;
using Contract.Services.Managers;
using Contract.ViewModels.UI;
using Contract.ViewModels.UI.Reports;
using MessageBox = System.Windows.Forms.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace Contract.Import.Reports
{
    /// <summary>
    ///     Interaction logic for ContractReport2Control.xaml
    /// </summary>
    public partial class ContractReport2Control : UserControl
    {
        private static readonly ReportManager Manager = new ReportManager();
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private ContractFilterModel _model;

        public ContractReport2Control()
        {
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

            DataGrid.Items.Add(new ContractReportSection1ReportModel());

            var model = (ContractReportSection2ReportModel)runWorkerCompletedEventArgs.Result;

            DataGrid.Items.Add(model);
        }

        private void WorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            var model = (ContractReportSection2ReportModel)progressChangedEventArgs.UserState;

            DataGrid.Items.Add(model);
        }

        private void WorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var models = Manager.GetSectionReport2Models(_model);

            foreach (var model in models)
            {
                _worker.ReportProgress(0, model);
            }

            doWorkEventArgs.Result = new ContractReportSection2ReportModel
            {
                Object = "Итого",
                Amount = models.Sum(s => s.Amount),
                Paid = models.Sum(s => s.Paid),
                Residue = models.Sum(s => s.Residue)
            };
        }

        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            if (_worker.IsBusy)
                return;

            _model = FilterControl.Model;
            //_model.Code = CategoryCode.Page2;

            if (!DataGrid.Items.IsEmpty)
                DataGrid.Items.Clear();

            _worker.RunWorkerAsync();
        }

        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            if (_model == null || DataGrid.Items.Count == 0)
            {
                MessageBox.Show(@"Please enter to filter fields", @"Filter", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var filePath = ExcelHelper.DoExportReport2(_model);

                Process.Start(filePath);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void DataGrid_OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}