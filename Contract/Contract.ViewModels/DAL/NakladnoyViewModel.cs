using System;

namespace Contract.ViewModels.DAL
{
    public class NakladnoyViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime? Date { get; set; }
        public int? ContractId { get; set; }
    }
}