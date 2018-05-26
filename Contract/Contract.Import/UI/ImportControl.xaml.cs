using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Contract.Data.Models.Enums;
using Contract.Services.Extensions;
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
    public partial class ImportControl : UserControl
    {
        private static readonly DalManager Manager = new DalManager();
        private readonly BackgroundWorker _worker = new BackgroundWorker();

        public ImportControl()
        {
            InitializeComponent();

            _worker.WorkerSupportsCancellation = true;
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += WorkerOnDoWork;
            _worker.ProgressChanged += WorkerOnProgressChanged;
            _worker.RunWorkerCompleted += WorkerOnRunWorkerCompleted;
        }

        private void WorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            if (runWorkerCompletedEventArgs.Cancelled)
            {
                MessageBox.Show(@"Operation cancelled", @"Cancel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (runWorkerCompletedEventArgs.Error != null)
            {
                MessageBox.Show(runWorkerCompletedEventArgs.Error.Message, @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                var models = (ImportDataModel)runWorkerCompletedEventArgs.Result;

                if (models != null)
                {
                    var sb = new StringBuilder();
                    for (var i = 1; i <= models.TotalPages; i++)
                    {
                        sb.AppendFormat("Page#{0} - {1} records", i, models.Items[(CategoryCode)i].Count).AppendLine();
                    }

                    MessageBox.Show(string.Format("{0} imported", sb), @"Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            Label.Content = "0%";
            Elapsed.Content = "0:0:0";
            ProgressBar.Value = 0;
        }

        private void WorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            var model = (ImportProcesModel)progressChangedEventArgs.UserState;

            ProgressBar.Value = model.Percent;
            Label.Content = string.Format("{0}%", model.Percent);
            Elapsed.Content = string.Format("{0:hh\\:mm\\:ss}", model.Elapsed);
            DataGrid.Items.Add(model.Item);
        }

        private void WorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var file = doWorkEventArgs.Argument.ToString();
            var stopWatch = new Stopwatch();
            var models = ExcelHelper.GetImportModels(file);
            var totalRows = models.TotalRows;
            var rowCounter = 0;
            var modelValues = new List<ImportModel>();

            stopWatch.Start();

            for (var pageIndex = 1; pageIndex <= models.TotalPages; pageIndex++)
            {
                var category = (CategoryCode)pageIndex;

                for (var rowIndex = 0; rowIndex < models.PageRows[category]; rowIndex++)
                {
                    if (rowIndex >= models.Items[category].Count)
                        continue;

                    var model = models.Items[category][rowIndex];

                    if (model == null)
                        continue; 

                    model.Category = category;
                    model.Branch = model.V9.GetBranchCode();
                    model.Status = Manager.DoImportModel(model);

                    rowCounter++;

                    var percent = Math.Round(100 * (double)rowCounter / totalRows, 2, MidpointRounding.AwayFromZero);

                    if (_worker.CancellationPending)
                    {
                        doWorkEventArgs.Cancel = true;
                        break;
                    }

                    _worker.ReportProgress(0, new ImportProcesModel
                    {
                        Percent = percent,
                        Elapsed = stopWatch.Elapsed,
                        Item = model
                    });

                    modelValues.Add(model);
                }

            }

            stopWatch.Stop();

            if (!doWorkEventArgs.Cancel)
                doWorkEventArgs.Result = modelValues;
        }

        private void btnExcelImport_Click(object sender, RoutedEventArgs e)
        {
            var file = TxtExcelPath.Text;

            if (string.IsNullOrWhiteSpace(file))
            {
                MessageBox.Show(@"Please choose excel file", @"Choose",
                    MessageBoxButtons.OK, MessageBoxIcon.Question);

                return;
            }

            if (!DataGrid.Items.IsEmpty)
                DataGrid.Items.Clear();

            if (_worker.IsBusy)
                return;

            _worker.RunWorkerAsync(file);
        }

        private void btnCancelImport_Click(object sender, RoutedEventArgs e)
        {
            if (_worker.CancellationPending)
                return;

            _worker.CancelAsync();
            Label.Content = "0%";
            ProgressBar.Value = 0;
        }

        private void ChooseButton_OnClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                DefaultExt = ".xlsx",
                Filter = @"Excel files (*.xlsx)|*.xlsx"
            };

            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                TxtExcelPath.Clear();
                ProgressBar.Value = 0;

                return;
            }

            TxtExcelPath.Text = fileDialog.FileName;
        }

        private class ImportProcesModel
        {
            public double Percent { get; set; }

            public TimeSpan Elapsed { get; set; }

            public ImportModel Item { get; set; }
        }

        private void DataGrid_OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}