using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contract.Data.Models.Base;
using Contract.Data.Models.Enums;

namespace Contract.Data.Models
{
    [Table("ActInvoices")]
    public class ActInvoice : BaseEntity
    {
        public string Number { get; set; }
         
        public string Description { get; set; }
 
        public int? ContractId { get; set; }

        [ForeignKey("ContractId")]
        public Contract Contract { get; set; }
         
        public InvoiceStatus Status { get; set; }

    }
}