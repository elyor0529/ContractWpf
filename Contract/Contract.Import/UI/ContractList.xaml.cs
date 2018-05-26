using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Contract.Core;
using Contract.Services.Interface;
using Contract.ViewModels.UI;
using Contract.ViewModels.UI.Reports;
using UserControl = System.Windows.Controls.UserControl;
using Contract.ViewModels.DAL;
using PagedList;

namespace Contract.Import.UI
{
    /// <summary>
    ///     Interaction logic for ImportControl.xaml
    /// </summary>
    public partial class ContractList : UserControl
    {
        private ContractFilterModel _model;
        private readonly IContractService _contractService;
        private int _topRowIndex;

        public ContractList()
        {
            _contractService = DiConfig.Resolve<IContractService>();

            InitializeComponent();

            // Create de Command.
            var changedIndex = new RoutedUICommand("ChangedIndex", "ChangedIndex", typeof(ExportControl));

            // Assing the command to PagingControl Command.
            GridPaging1.ChangedIndexCommand = changedIndex;

            // Binding Command
            var binding = new CommandBinding
            {
                Command = changedIndex
            };

            // Binding Handler to executed.
            binding.Executed += OnChangeIndexCommandHandler;

            CommandBindings.Add(binding);

            ExecuteQuery();
        }


        private void ExecuteQuery()
        {

            var pageIndex = GridPaging1.PageIndex;
            var pageSize = GridPaging1.PageSize;
            var models = _contractService.Paging(_model, pageIndex, pageSize);

            DataGrid.Items.Clear();

            foreach (var model in models)
                DataGrid.Items.Add(model);

            DataGrid.Items.Add(new ContractReportModel());

            DataGrid.Items.Add(new ContractReportModel
            {
                Object = "Итого",
                Amount = models.Sum(s => s.Amount)
            });

            _topRowIndex = (pageIndex - 1) * pageSize;


            GridPaging1.TotalCount = models.TotalItemCount;
        }

        private void OnChangeIndexCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            ExecuteQuery();
        }

        private void filterButton_Click(object sender, RoutedEventArgs e)
        {

            _model = FilterControl.Model;

            ExecuteQuery();

        }


        private void exportButton_Click(object sender, System.Windows.RoutedEventArgs e)
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
            e.Row.Header = (_topRowIndex + e.Row.GetIndex() + 1).ToString();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var model = (ContractReportModel)DataGrid.SelectedItem;

            if (model == null)
                return;

            var window = new ContractForm(model.Id);
            var result = window.ShowDialog();

            if (result.HasValue && result.Value)
            {
                filterButton_Click(sender, null);
            }
        }

        private void NewButton_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new ContractForm();
            var result = window.ShowDialog();

            if (result.HasValue && result.Value)
            {
                filterButton_Click(sender, null);
            }
        }

        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            //TODO: Need to impl.sk
        }
    }
}