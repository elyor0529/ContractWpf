using System;
using System.Windows;
using System.Windows.Forms;
using Contract.Core;
using Contract.Import.ViewModels;
using Contract.Services.Interface;
using Contract.ViewModels.DAL;

namespace Contract.Import.UI
{
    /// <summary>
    /// Interaction logic for CustomerForm.xaml
    /// </summary>
    public partial class CustomerForm : Window
    {
        private readonly IOrganizationService _organizationService;

        public CustomerForm()
        {
            _organizationService = DiConfig.Resolve<IOrganizationService>();

            CustomerModel = new OrganizationUIViewModel();

            InitializeComponent();

            CustomerModel.Validate();
        }

        public CustomerForm(int id)
        {
            _organizationService = DiConfig.Resolve<IOrganizationService>();

            var organization = _organizationService.GetById(id);

            CustomerModel = new OrganizationUIViewModel
            {
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
            };

            InitializeComponent();
        }

        public OrganizationUIViewModel CustomerModel { get; set; }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            CustomerModel.Validate();

            if (CustomerModel.HasErrors)
            {
                System.Windows.Forms.MessageBox.Show(@"Не проверять", @"Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var organization = new OrganizationViewModel
                {
                    Id = CustomerModel.Id,
                    Name = CustomerModel.Name,
                    PhoneNumbers = CustomerModel.PhoneNumbers,
                    AccountNumber = CustomerModel.AccountNumber,
                    BankName1 = CustomerModel.BankName1,
                    Mfo1 = CustomerModel.Mfo1,
                    BankName2 = CustomerModel.BankName2,
                    Mfo2 = CustomerModel.Mfo2,
                    BankName3 = CustomerModel.BankName3,
                    Mfo3 = CustomerModel.Mfo3,
                    Position = CustomerModel.Position,
                    Chief = CustomerModel.Chief,
                    Foundation = CustomerModel.Foundation,
                    KS = CustomerModel.KS,
                    LS = CustomerModel.LS,
                    Postcode = CustomerModel.Postcode,
                    TypeOwnership = CustomerModel.TypeOwnership,
                    Okohx = CustomerModel.Okohx,
                    Inn = CustomerModel.Inn,
                    LegalAddress = CustomerModel.Okohx
                };

                _organizationService.CreateOrUpdate(organization);

                DataContext = new OrganizationUIViewModel();

                System.Windows.Forms.MessageBox.Show(@"Успешно сохранено", @"Сохранить", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = true;
            }
            catch (Exception exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.Message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DataContext = new OrganizationUIViewModel();

            Close();
        }
    }
}
