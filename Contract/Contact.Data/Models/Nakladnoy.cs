using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contract.Data.Models.Base;

namespace Contract.Data.Models
{
    [Table("Nakladnoyes")]
    public class Nakladnoy : BaseEntity
    {
        public string Number { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Date { get; set; }

        public int? ContractId { get; set; }

        [ForeignKey("ContractId")]
        public Contract Contract { get; set; }
    }
}