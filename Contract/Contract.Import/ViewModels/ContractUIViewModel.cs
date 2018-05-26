using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contract.Services.Interface;
using Contract.ViewModels.DAL;

namespace Contract.Import.ViewModels {
    public class ContractUIViewModel : UIViewModelBase {

        private void CalculateAmount() {
            _contractAmount = _amount1 + _amount2 + _amount3 + _amount4 + _amount5;
        }

        private int? _branchId;

        private int? _categoryId;

        private DateTime? _contractDate;

        private bool? _isTreaguare;

        private string _objectName;

        private OrganizationUIViewModel _organization;

        private bool? _hasActInvioce;

        private bool? _hasNakladnoy;

        private IPeriodService PeriodService {
            get { return GetService<IPeriodService>(); }
        }

        private IWorkTypeService WorkTypeService {
            get { return GetService<IWorkTypeService>(); }
        }

        private ICategoryService CategoryService {
            get { return GetService<ICategoryService>(); }
        }

        private IContractService ContractService {
            get { return GetService<IContractService>(); }
        }

        private IBranchService BranchService {
            get { return GetService<IBranchService>(); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This '{0}' field is required")]
        public int? BranchId {
            get { return _branchId; }
            set {
                _branchId = value;

                RaisePropertyChanged();

                if (Id == 0) {
                    RaisePropertyChanged("ContractNumber");
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This '{0}' field is required")]
        public int? CategoryId {
            get { return _categoryId; }
            set {
                _categoryId = value;

                RaisePropertyChanged();

                if (Id == 0) {
                    RaisePropertyChanged("ContractNumber");
                }
            }
        }

        public string ContractNumber {
            get {
                if (_branchId == null || _categoryId == null)
                    return string.Empty;

                if (Id == 0) {
                    var branch = BranchService.GetById(_branchId ?? 0);
                    var category = CategoryService.GetById(_categoryId ?? 0);
                    var counter = category.Counter + 1;

                    return string.Format("{0}/{1}-{2}", (int)branch.Code, (int)category.Code, counter);
                }

                var contract = ContractService.GetById(Id);

                return contract.ContractNumber;
            }
            set {
                RaisePropertyChanged();
            }
        }
        public OrganizationUIViewModel Organization {
            get { return _organization; }
            set {
                _organization = value;

                RaisePropertyChanged();
            }
        }

        public DateTime? ContractDate {
            get { return _contractDate; }
            set {
                _contractDate = value;

                RaisePropertyChanged();
            }
        }

        public const double TREAGUARE_PERCENT_FOR_AMOUNT = 0.15;

        public bool? IsTreaguare {
            get { return _isTreaguare; }
            set {
                _isTreaguare = value;

                RaisePropertyChanged();
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This '{0}' field is required")]
        public string ObjectName {
            get { return _objectName; }
            set {
                _objectName = value;

                RaisePropertyChanged();
            }
        }

        public IList<BranchViewModel> Branches {
            get { return BranchService.GetAll(); }
        }

        public IList<CategoryViewModel> Categories {
            get { return CategoryService.GetAll(); }
        }

        public IList<WorkTypeViewModel> WorkTypes {
            get { return WorkTypeService.GetAll(); }
        }

        public IList<PeriodViewModel> Periods {
            get { return PeriodService.GetAll(); }
        }


        private int? _workTypeId1;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This '{0}' field is required")]
        public int? WorkTypeId1 {
            get { return _workTypeId1; }
            set {
                _workTypeId1 = value;

                RaisePropertyChanged();
            }
        }

        private double? _amount1 = 0;

        public double? Amount1 {
            get { return _amount1; }
            set {
                _amount1 = value;

                RaisePropertyChanged();
                RaisePropertyChanged("ContractAmount");
            }
        }

        private int? _periodId1;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This '{0}' field is required")]
        public int? PeriodId1 {
            get { return _periodId1; }
            set {
                _periodId1 = value;

                RaisePropertyChanged();
            }
        }


        private int? _workTypeId2;

        public int? WorkTypeId2 {
            get { return _workTypeId2; }
            set {
                _workTypeId2 = value;

                RaisePropertyChanged();
            }
        }

        private double? _amount2 = 0;

        public double? Amount2 {
            get { return _amount2; }
            set {
                _amount2 = value;

                RaisePropertyChanged();
                RaisePropertyChanged("ContractAmount");
            }
        }

        private int? _periodId2;

        public int? PeriodId2 {
            get { return _periodId2; }
            set {
                _periodId2 = value;

                RaisePropertyChanged();
            }
        }


        private int? _workTypeId3;

        public int? WorkTypeId3 {
            get { return _workTypeId3; }
            set {
                _workTypeId3 = value;

                RaisePropertyChanged();
            }
        }

        private double? _amount3 = 0;

        public double? Amount3 {
            get { return _amount3; }
            set {
                _amount3 = value;

                RaisePropertyChanged();
                RaisePropertyChanged("ContractAmount");
            }
        }

        private int? _periodId3;

        public int? PeriodId3 {
            get { return _periodId3; }
            set {
                _periodId3 = value;

                RaisePropertyChanged();
            }
        }


        private int? _workTypeId4;

        public int? WorkTypeId4 {
            get { return _workTypeId4; }
            set {
                _workTypeId4 = value;

                RaisePropertyChanged();
            }
        }

        private double? _amount4 = 0;

        public double? Amount4 {
            get { return _amount4; }
            set {
                _amount4 = value;

                RaisePropertyChanged();
                RaisePropertyChanged("ContractAmount");
            }
        }

        private int? _periodId4;

        public int? PeriodId4 {
            get { return _periodId4; }
            set {
                _periodId4 = value;

                RaisePropertyChanged();
            }
        }

        private int? _workTypeId5;

        public int? WorkTypeId5 {
            get { return _workTypeId5; }
            set {
                _workTypeId5 = value;

                RaisePropertyChanged();
            }
        }

        private double? _amount5 = 0;

        public double? Amount5 {
            get { return _amount5; }
            set {
                _amount5 = value;

                RaisePropertyChanged();
                RaisePropertyChanged("ContractAmount");
            }
        }

        private int? _periodId5;

        public int? PeriodId5 {
            get { return _periodId5; }
            set {
                _periodId5 = value;

                RaisePropertyChanged();
            }
        }

        private double? _contractAmount;
        [Range(1, Int32.MaxValue, ErrorMessage = "Сумма контракта более '0'")]
        public double? ContractAmount {
            get {
                CalculateAmount();

                return _contractAmount;
            }
            set {

                _contractAmount = value;

                RaisePropertyChanged();
            }
        }

        public bool? HasActInvoice {
            get { return _hasActInvioce; }
            set {
                _hasActInvioce = value;

                RaisePropertyChanged();
            }
        }

        public bool? HasNakladnoy {
            get { return _hasNakladnoy; }
            set {
                _hasNakladnoy = value;

                RaisePropertyChanged();
            }
        }

        public int Id { get; set; }

        public ContractUIViewModel() {
            _organization = new OrganizationUIViewModel();
        }


    }
}