using Contract.Data.Models;
using Contract.Data.Models.Enums;

namespace Contract.ViewModels.DAL
{
    public class ContractWorkPaymentViewModel
    {
        public int Id { get; set; }
        public int? ContractId { get; set; }
        public int? WorkTypeId { get; set; } 
        public double? Amount { get; set; }
        public int? PeriodId { get; set; }
    }
}