using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using Contract.Core;
using Contract.Core.Extensions;
using Contract.Import.ViewModels;
using Contract.Services.Interface;
using Contract.ViewModels.DAL;
using TextBox = System.Windows.Controls.TextBox;
using GmailClient.Framework.Extensions;
using Contract.Services.Helpers;
using System.Diagnostics;

namespace Contract.Import.UI {
    /// <summary>
    /// Interaction logic for ContractControl.xaml
    /// </summary>
    public partial class ContractForm : Window {
        private readonly IContractService _contractService;
        private readonly ICategoryService _categoryService;
        private readonly IContractWorkPaymentService _workPaymentService;
        private readonly IPaymentService _paymentService;
        private readonly INakladnoyService _nakladnoyService;
        private readonly IActInvoiceService _actInvoiceService;
        private readonly IOrganizationService _organizationService;

        private int SaveContract() {
            ContractModel.Validate();

            if (ContractModel.HasErrors) {
                System.Windows.Forms.MessageBox.Show(@"Пожалуйста, введите все красные цветовые поля", @"Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            if (ContractModel.IsTreaguare != true &&
                ContractModel.ContractDate == null) {
                System.Windows.Forms.MessageBox.Show(@"Выберите дату контракта", @"Дата", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            try {
                var contractViewModel = new ContractViewModel {
                    Id = ContractModel.Id,
                    BranchId = ContractModel.BranchId,
                    CategoryId = ContractModel.CategoryId,
                    ContractNumber = ContractModel.ContractNumber,
                    OrganizationId = ContractModel.Organization.Id,
                    ContractAmount = ContractModel.ContractAmount,
                    ContractDate = ContractModel.ContractDate,
                    ObjectName = ContractModel.ObjectName
                };
                var contractId = _contractService.CreateOrUpdate(contractViewModel);

                //category
                if (contractViewModel.Id == 0) {
                    var category = _categoryService.GetById(contractViewModel.CategoryId ?? 0);

                    category.Counter = category.Counter + 1;

                    _categoryService.CreateOrUpdate(category);
                }

                //work types 
                var workTypes = new ContractWorkPaymentViewModel[]
                {
                    #region items
                    new ContractWorkPaymentViewModel
                    {
                        Amount = ContractModel.Amount1,
                        PeriodId = ContractModel.PeriodId1,
                        ContractId = contractId,
                        WorkTypeId = ContractModel.WorkTypeId1
                    },
                    new ContractWorkPaymentViewModel
                    {
                        Amount = ContractModel.Amount2,
                        PeriodId = ContractModel.PeriodId2,
                        ContractId = contractId,
                        WorkTypeId = ContractModel.WorkTypeId2
                    },
                    new ContractWorkPaymentViewModel
                    {
                        Amount = ContractModel.Amount3,
                        PeriodId = ContractModel.PeriodId3,
                        ContractId = contractId,
                        WorkTypeId = ContractModel.WorkTypeId3
                    },
                    new ContractWorkPaymentViewModel
                    {
                        Amount = ContractModel.Amount4,
                        PeriodId = ContractModel.PeriodId4,
                        ContractId = contractId,
                        WorkTypeId = ContractModel.WorkTypeId4
                    },
                    new ContractWorkPaymentViewModel
                    {
                        Amount = ContractModel.Amount5,
                        PeriodId = ContractModel.PeriodId5,
                        ContractId = contractId,
                        WorkTypeId = ContractModel.WorkTypeId5
                    } 
                    #endregion
                };

                //work type
                foreach (var workType in workTypes) {
                    var workTypeItem = _workPaymentService.Get(f => f.ContractId == ContractModel.Id &&
                        f.WorkTypeId == workType.WorkTypeId);

                    if (workTypeItem != null)
                        workType.Id = workTypeItem.Id;
                    _workPaymentService.CreateOrUpdate(workType);
                }

                if (ContractModel.IsTreaguare == true && contractViewModel.Id == 0) {
                    //payment
                    var payment = new PaymentViewModel {
                        Amount = ContractModel.ContractAmount * ContractUIViewModel.TREAGUARE_PERCENT_FOR_AMOUNT,
                        Date = ContractModel.ContractDate,
                        ContractId = contractId
                    };

                    _paymentService.CreateOrUpdate(payment);
                }


                //nakladnoy
                if (ContractModel.HasNakladnoy == true) {
                    var nakladnoy = new NakladnoyViewModel {
                        ContractId = contractId,
                        Date = ContractModel.ContractDate,
                        Number = ContractModel.ContractNumber
                    };
                    var nakladnoyItem = _nakladnoyService.Get(f => f.ContractId == ContractModel.Id);

                    if (nakladnoyItem != null)
                        nakladnoy.Id = nakladnoyItem.Id;

                    _nakladnoyService.CreateOrUpdate(nakladnoy);
                }

                // act invoice
                if (ContractModel.HasActInvoice == true) {
                    var actInvoice = new ActInvoiceViewModel {
                        ContractId = contractId,
                        Number = ContractModel.ContractNumber,
                        Amount = ContractModel.ContractAmount
                    };
                    var actInvoiceItem = _actInvoiceService.Get(f => f.ContractId == ContractModel.Id);

                    if (actInvoiceItem != null)
                        actInvoice.Id = actInvoiceItem.Id;

                    _actInvoiceService.CreateOrUpdate(actInvoice);
                }


                System.Windows.Forms.MessageBox.Show(@"Успешно сохранено", @"Сохранить", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Grid1.DataContext = new ContractUIViewModel();

                DialogResult = true;

                return contractId;
            } catch (Exception exception) {
                System.Windows.Forms.MessageBox.Show(exception.Message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return 0;
        }

        public ContractForm() {
            _contractService = DiConfig.Resolve<IContractService>();
            _categoryService = DiConfig.Resolve<ICategoryService>();
            _workPaymentService = DiConfig.Resolve<IContractWorkPaymentService>();
            _paymentService = DiConfig.Resolve<IPaymentService>();
            _nakladnoyService = DiConfig.Resolve<INakladnoyService>();
            _actInvoiceService = DiConfig.Resolve<IActInvoiceService>();
            _organizationService = DiConfig.Resolve<IOrganizationService>();

            ContractModel = new ContractUIViewModel();

            InitializeComponent();

            ContractModel.Validate();

            Grid1.DataContext = ContractModel;

        }

        public ContractForm(int id) {
            _contractService = DiConfig.Resolve<IContractService>();
            _workPaymentService = DiConfig.Resolve<IContractWorkPaymentService>();
            _paymentService = DiConfig.Resolve<IPaymentService>();
            _nakladnoyService = DiConfig.Resolve<INakladnoyService>();
            _actInvoiceService = DiConfig.Resolve<IActInvoiceService>();
            _organizationService = DiConfig.Resolve<IOrganizationService>();

            var model = _contractService.GetById(id);
            var organization = _organizationService.GetById(model.OrganizationId ?? 0);
            var workTypes = _workPaymentService.GetAll(a => a.ContractId == model.Id);

            ContractModel = new ContractUIViewModel {
                Id = id,
                BranchId = model.BranchId,
                CategoryId = model.CategoryId,
                ContractAmount = model.ContractAmount,
                ContractDate = model.ContractDate,
                ContractNumber = model.ContractNumber,
                HasActInvoice = model.ActInvoices.Count > 0,
                HasNakladnoy = model.Nakladnoys.Count > 0,
                IsTreaguare = model.IsTriguare,
                ObjectName = model.ObjectName,
                Organization = new OrganizationUIViewModel {
                    Id = organization.Id,
                    AccountNumber = organization.AccountNumber,
                    Okohx = organization.Okohx,
                    Name = organization.Name,
                    Inn = organization.Inn,
                    BankName1 = organization.BankName1,
                    Mfo1 = organization.Mfo1,
                    BankName2 = organization.BankName2,
                    Mfo2 = organization.Mfo2,
                    BankName3 = organization.BankName3,
                    Mfo3 = organization.Mfo3,
                    PhoneNumbers = organization.PhoneNumbers,
                    Position = organization.Position,
                    Chief = organization.Chief,
                    KS = organization.KS,
                    LS = organization.LS,
                    Foundation = organization.Foundation,
                    Postcode = organization.Postcode,
                    TypeOwnership = organization.TypeOwnership,
                    LegalAddress = organization.LegalAddress
                },
                WorkTypeId1 = workTypes[0].WorkTypeId,
                Amount1 = workTypes[0].Amount,
                PeriodId1 = workTypes[0].PeriodId,

                WorkTypeId2 = workTypes[1].WorkTypeId,
                Amount2 = workTypes[1].Amount,
                PeriodId2 = workTypes[1].PeriodId,

                WorkTypeId3 = workTypes[2].WorkTypeId,
                Amount3 = workTypes[2].Amount,
                PeriodId3 = workTypes[2].PeriodId,

                WorkTypeId4 = workTypes[3].WorkTypeId,
                Amount4 = workTypes[3].Amount,
                PeriodId4 = workTypes[3].PeriodId,

                WorkTypeId5 = workTypes[4].WorkTypeId,
                Amount5 = workTypes[4].Amount,
                PeriodId5 = workTypes[4].PeriodId
            };

            InitializeComponent();

            ContractModel.Validate();

            Grid1.DataContext = ContractModel;
            ContractTreaguare.IsEnabled = false;
        }

        public ContractUIViewModel ContractModel { get; set; }

        private void btnCustomer_Click(object sender, RoutedEventArgs e) {
            var window = new CustomerList();
            var result = window.ShowDialog();

            if (result.HasValue && result.Value) {
                var organization = (OrganizationUIViewModel)window.DataContext;

                ContractModel.Organization = organization;
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e) {
            SaveContract();
        }  

        private void btnSaveAndPrint_Click(object sender, RoutedEventArgs e) {
            var contractId = SaveContract();

            try {
                var path = WordHelper.ContractExport(contractId);

                Process.Start(path);
            } catch (Exception exception) {
                System.Windows.Forms.MessageBox.Show(exception.Message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ContractTreaguare_Click(object sender, RoutedEventArgs e) {
            ContractModel.IsTreaguare = ContractTreaguare.IsChecked;

            if (ContractTreaguare.IsChecked != true) {
                ContractModel.ContractDate = DateTime.Now;
            } else {
                ContractModel.ContractDate = null;
            }

        }
    }
}
