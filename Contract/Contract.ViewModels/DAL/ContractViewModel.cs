using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Contract.Data.Models;
using Contract.Data.Models.Enums;

namespace Contract.ViewModels.DAL
{
    public class ContractViewModel
    {

        public int Id { get; set; }

        [DisplayName("Наименование объекта")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string ObjectName { get; set; }

        [DisplayName("№ договора")]
        [Required]
        public string ContractNumber { get; set; }

        [DisplayName("Дата договора")]
        [Required]
        public DateTime? ContractDate { get; set; }

        [DisplayName("Сумма договора")]
        [DataType(DataType.Currency)]
        [Required]
        public double? ContractAmount { get; set; }

        [DisplayName("Сроки сдачи работ по договору")]
        public DateTime? DeadlinesContract { get; set; }

        [DisplayName("Сроки сдачи работ по факту")]
        public DateTime? DeadlinesFact { get; set; }

        [DisplayName("Недостатки в комплектации экономического дела")]
        [DataType(DataType.MultilineText)]
        public string Limitation { get; set; }

        [DisplayName("Организация")]
        [Required]
        public int? OrganizationId { get; set; }

        [DisplayName("Страница")]
        [Required]
        public int? CategoryId { get; set; }

        public int? BranchId { get; set; }

        public IList<ContractDepartmentViewModel> ContractDepartments { get; set; }

        public IList<ActInvoiceViewModel> ActInvoices { get; set; }

        public IList<PaymentViewModel> Payments { get; set; }

        public IList<NakladnoyViewModel> Nakladnoys { get; set; }

        public IList<ContractWorkPaymentViewModel> ContractWorkPayments { get; set; }

        public ContractViewModel()
        {
            ContractDepartments = new List<ContractDepartmentViewModel>();
            ActInvoices = new List<ActInvoiceViewModel>();
            Payments = new List<PaymentViewModel>();
            Nakladnoys = new List<NakladnoyViewModel>();
            ContractWorkPayments = new List<ContractWorkPaymentViewModel>();
        }


        public bool? IsTriguare { get; set; }
    }
}