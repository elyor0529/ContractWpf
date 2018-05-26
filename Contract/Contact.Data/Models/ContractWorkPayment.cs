using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contract.Data.Models.Base;
using Contract.Data.Models.Enums;

namespace Contract.Data.Models
{
    [Table("ContractWorkPayments")]
    public class ContractWorkPayment : BaseEntity
    {
        public int? ContractId { get; set; }

        [ForeignKey("ContractId")]
        public Contract Contract { get; set; }

        public int? WorkTypeId { get; set; }

        [ForeignKey("WorkTypeId")]
        public WorkType WorkType { get; set; }

        public double? Amount { get; set; }

        public int? PeriodId { get; set; }

        [ForeignKey("PeriodId")]
        public Period Period { get; set; }

    }
}