using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Contract.Data.Models.Base;
using Contract.Data.Models.Enums;

namespace Contract.Data.Models
{
    [Table("WorkTypes")]
    public class WorkType : BaseEntity
    {
        public WorkTypeCode Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } 
        public ICollection<ContractWorkPayment> ContractWorkPayments { get; set; }

        public WorkType()
        {
            ContractWorkPayments = new List<ContractWorkPayment>();
        }

    }
}