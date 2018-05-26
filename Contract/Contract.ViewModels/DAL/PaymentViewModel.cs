using System;

namespace Contract.ViewModels.DAL
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public double? Amount { get; set; }
        public string Description { get; set; }
        public int? ContractId { get; set; }
    }
}