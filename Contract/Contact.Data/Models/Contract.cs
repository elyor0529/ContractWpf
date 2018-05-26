using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contract.Data.Models.Base;
using Contract.Data.Models.Enums;

namespace Contract.Data.Models
{
    [Table("Contracts")]
    public class Contract : BaseEntity
    {
        public string ObjectName { get; set; }

        public string ContractNumber { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ContractDate { get; set; }

        public double? ContractAmount { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeadlinesContract { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeadlinesFact { get; set; }

        public string Limitation { get; set; }

        public int? OrganizationId { get; set; }

        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int? BranchId { get; set; }

        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }

        public bool? IsTriguare { get; set; }
         
        public ICollection<ContractDepartment> ContractDepartments { get; set; }

        public ICollection<ActInvoice> ActInvoices { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public ICollection<Nakladnoy> Nakladnoys { get; set; }

        public ICollection<ContractWorkPayment> ContractWorkPayments { get; set; }

        public Contract()
        {
            ContractDepartments = new List<ContractDepartment>();
            ActInvoices = new List<ActInvoice>();
            Payments = new List<Payment>();
            Nakladnoys = new List<Nakladnoy>();
            ContractWorkPayments = new List<ContractWorkPayment>();
        }

    }
}