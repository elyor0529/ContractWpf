using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contract.Data.Models.Base;

namespace Contract.Data.Models
{
    [Table("Payments")]
    public class Payment : BaseEntity
    {

        [Column(TypeName = "datetime2")]
        public DateTime? Date { get; set; }

        public double? Amount { get; set; }

        public string Description { get; set; }

        public int? ContractId { get; set; }

        [ForeignKey("ContractId")]
        public Contract Contract { get; set; }

    }
}