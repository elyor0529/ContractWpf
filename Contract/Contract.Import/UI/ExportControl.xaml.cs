using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Contract.Services.Helpers;
using Contract.Services.Managers;
using Contract.ViewModels.UI;
using MessageBox = System.Windows.Forms.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace Contract.Import.UI
{
    /// <summary>
    ///     Interaction logic for ImportControl.xaml
    /// </summary>
    public partial class ExportControl : UserControl
    {
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private ContractFilterModel _model;
        private static readonly ReportManager ReportManager = new ReportManager();
        private static readonly DalManager DalManager = new DalManager();

        /// <summary>
        /// List of orders.
        /// </summary>
        private IList<ExportModel> _models;

        private int _topRowIndex;


        public ExportControl()
        {
            InitializeComponent();

            _worker.DoWork += WorkerOnDoWork;
            _worker.RunWorkerCompleted += WorkerOnRunWorkerCompleted;

            // Create de Command.
            var changedIndex = new RoutedUICommand("ChangedIndex", "ChangedIndex", typeof(ExportControl));

            // Assing the command to PagingControl Command.
            GridPaging1.ChangedIndexCommand = changedIndex;

            // Binding Command
            var binding = new CommandBinding { Command = changedIndex };

            // Binding Handler to executed.
            binding.Executed += OnChangeIndexCommandHandler;

            CommandBindings.Add(binding);
        }


        /// <summary>
        /// Refactoring for Get the query.
        /// </summary>
        /// <param name="pageIndex">
        /// The page index.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// The row count.
        /// </returns>
        private int ExecuteQueryReturnTotalItem(int pageIndex, int pageSize)
        {
            // Calculating the initial and final row.
            var topRow = (pageIndex - 1) * pageSize;
            var totalCount = _models.Count;
            var models = _models.Skip(topRow).Take(pageSize).ToList();

            foreach (var model in models)
            {
                DataGrid.Items.Add(model);
            }

            _topRowIndex = topRow;

            return totalCount;
        }


        /// <summary>
        /// Get the change index event.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnChangeIndexCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            var pageIndex = GridPaging1.PageIndex;
            var pageSize = GridPaging1.PageSize;

            DataGrid.Items.Clear();
            GridPaging1.TotalCount = ExecuteQueryReturnTotalItem(pageIndex, pageSize);
        }

        private void WorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {

            GridPaging1.ResetPageIndex();
            DataGrid.Items.Clear();

            var pageIndex = GridPaging1.PageIndex;
            var pageSize = GridPaging1.PageSize;

            _models = (IList<ExportModel>)runWorkerCompletedEventArgs.Result;

            GridPaging1.TotalCount = ExecuteQueryReturnTotalItem(pageIndex, pageSize);
        }

        private void WorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var models = DalManager.GetExportModels(_model);

            doWorkEventArgs.Result = models;
        }

        private void filterButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_worker.IsBusy)
                return;

            _model = FilterControl.Model;

            _worker.RunWorkerAsync();
        }


        private void exportButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_model == null || DataGrid.Items.Count == 0)
            {
                MessageBox.Show(@"Please enter to filter fields", @"Filter", MessageBoxButtons.OK,
                 MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var pageIndex = GridPaging1.PageIndex;
                var pageSize = GridPaging1.PageSize;
                var topRow = (pageIndex - 1) * pageSize;

                var filePath = ExcelHelper.DoExport(_model, topRow, pageSize);

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
            e.Row.Header = (_topRowIndex + e.Row.GetIndex() + 1).ToString();
        }
    }
}