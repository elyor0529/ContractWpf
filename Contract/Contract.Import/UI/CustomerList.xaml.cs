using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Contract.Core;
using Contract.Import.ViewModels;
using Contract.Services.Interface;
using Contract.ViewModels.DAL;

namespace Contract.Import.UI
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerList : Window
    {
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private readonly IOrganizationService _organizationService;
        private IList<OrganizationViewModel> _models;
        private string _search;

        public CustomerList()
        {
            InitializeComponent();

            _organizationService = DiConfig.Resolve<IOrganizationService>();

            _worker.DoWork += WorkerOnDoWork;
            _worker.RunWorkerCompleted += WorkerOnRunWorkerCompleted;

            // Create de Command.
            var changedIndex = new RoutedUICommand("ChangedIndex", "ChangedIndex", typeof(CustomerList));

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

            _worker.RunWorkerAsync();

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
                DataGrid1.Items.Add(model);
            }

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

            DataGrid1.Items.Clear();
            GridPaging1.TotalCount = ExecuteQueryReturnTotalItem(pageIndex, pageSize);
        }

        private void WorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var query = _organizationService.GetAll();

            if (!String.IsNullOrWhiteSpace(_search))
                query = query.Where(a => a.Inn.IndexOf(_search, StringComparison.InvariantCultureIgnoreCase) != -1 ||
                                         a.Name.IndexOf(_search, StringComparison.InvariantCultureIgnoreCase) != -1)
                    .ToList();

            var models = query
                .OrderByDescending(o => o.Id)
                .ToList();

            doWorkEventArgs.Result = models;
        }

        private void WorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {

            GridPaging1.ResetPageIndex();
            DataGrid1.Items.Clear();

            var pageIndex = GridPaging1.PageIndex;
            var pageSize = GridPaging1.PageSize;

            _models = (IList<OrganizationViewModel>)runWorkerCompletedEventArgs.Result;

            GridPaging1.TotalCount = ExecuteQueryReturnTotalItem(pageIndex, pageSize);
        }

        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItem == null)
                return;

            var model = (OrganizationViewModel)DataGrid1.SelectedItem;

            DataContext = new OrganizationUIViewModel
            {
                Id = model.Id,
                AccountNumber = model.AccountNumber,
                BankName1 = model.BankName1,
                Mfo1 = model.Mfo1,
                BankName2 = model.BankName2,
                Mfo2 = model.Mfo2,
                BankName3 = model.BankName3,
                Mfo3 = model.Mfo3,
                Inn = model.Inn,
                Name = model.Name,
                Okohx = model.Okohx,
                PhoneNumbers = model.PhoneNumbers,
                Chief = model.Chief,
                Foundation = model.Foundation,
                KS = model.KS,
                LS = model.LS,
                LegalAddress = model.LegalAddress,
                Position = model.Position,
                Postcode = model.Postcode,
                TypeOwnership = model.TypeOwnership
            };
            DialogResult = true;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new CustomerForm();
            var result = window.ShowDialog();

            if (result.HasValue && result.Value)
            {
                _worker.RunWorkerAsync();
            }
        }

        private void DataGrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ButtonSelect_Click(sender, null);
        }

        private void TextBox1_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_worker.IsBusy)
                return;

            _search = TextBox1.Text;
            _worker.RunWorkerAsync();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            var model = (OrganizationViewModel)DataGrid1.SelectedItem;

            if (model == null)
                return;

            var window = new CustomerForm(model.Id);
            var result = window.ShowDialog();

            if (result.HasValue && result.Value)
            {
                _worker.RunWorkerAsync();
            }
        }
    }
}
