using System.ComponentModel.DataAnnotations;

namespace Contract.Import.ViewModels
{
    public class OrganizationUIViewModel : UIViewModelBase
    {
        public int Id { get; set; }

        private string _typeOwnership;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This '{0}' field is required")]
        public string TypeOwnership
        {
            get { return _typeOwnership; }
            set
            {
                _typeOwnership = value;

                RaisePropertyChanged();
            }
        }

        private string _chief;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This '{0}' field is required")]
        public string Chief
        {
            get { return _chief; }
            set
            {
                _chief = value;

                RaisePropertyChanged();
            }
        }

        private string _position;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This '{0}' field is required")]
        public string Position
        {
            get { return _position; }
            set
            {
                _position = value;

                RaisePropertyChanged();
            }
        }

        private string _foundation;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This '{0}' field is required")]
        public string Foundation
        {
            get { return _foundation; }
            set
            {
                _foundation = value;

                RaisePropertyChanged();
            }
        }

        private string _legalAddress;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This '{0}' field is required")]
        public string LegalAddress
        {
            get { return _legalAddress; }
            set
            {
                _legalAddress = value;

                RaisePropertyChanged();
            }
        }

        private string _postcode;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This '{0}' field is required")]
        public string Postcode
        {
            get { return _postcode; }
            set
            {
                _postcode = value;

                RaisePropertyChanged();
            }
        }

        private string _name;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This '{0}' field is required")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;

                RaisePropertyChanged();
            }
        }

        private string _inn;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This '{0}' field is required")]
        [StringLength(9, ErrorMessage = "The '{0}' value cannot exceed 9 numbers")]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "'{0}' is required and must be properly formatted")]
        public string Inn
        {
            get { return _inn; }
            set
            {
                _inn = value;

                RaisePropertyChanged();

            }
        }

        private string _accountNumber;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This '{0}' field is required")]
        public string AccountNumber
        {
            get { return _accountNumber; }
            set
            {
                _accountNumber = value;

                RaisePropertyChanged();
            }
        }

        private string _okohx;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This '{0}' field is required")]
        public string Okohx
        {
            get { return _okohx; }
            set
            {
                _okohx = value;

                RaisePropertyChanged();
            }
        }

        private string _phoneNumbers;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This '{0}' field is required")]
        public string PhoneNumbers
        {
            get { return _phoneNumbers; }
            set
            {
                _phoneNumbers = value;

                RaisePropertyChanged();
            }
        }

        private string _bankName1;

        public string BankName1
        {
            get { return _bankName1; }
            set
            {
                _bankName1 = value;

                RaisePropertyChanged();
            }
        }

        private string _mfo1;

        public string Mfo1
        {
            get { return _mfo1; }
            set
            {
                _mfo1 = value;

                RaisePropertyChanged();
            }
        }

        private string _ls;

        public string LS
        {
            get { return _ls; }
            set { _ls = value; }
        }

        private string _bankName2;

        public string BankName2
        {
            get { return _bankName2; }
            set
            {
                _bankName2 = value;

                RaisePropertyChanged();
            }
        }

        private string _mfo2;

        public string Mfo2
        {
            get { return _mfo2; }
            set
            {
                _mfo2 = value;

                RaisePropertyChanged();
            }
        }

        private string _ks;

        public string KS
        {
            get { return _ks; }
            set { _ks = value; }
        }


        private string _bankName3;

        public string BankName3
        {
            get { return _bankName3; }
            set
            {
                _bankName3 = value;

                RaisePropertyChanged();
            }
        }

        private string _mfo3;

        public string Mfo3
        {
            get { return _mfo3; }
            set
            {
                _mfo3 = value;

                RaisePropertyChanged();
            }
        }

    }
}