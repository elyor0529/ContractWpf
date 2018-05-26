namespace Contract.ViewModels.DAL
{
    public class ActInvoiceViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public double? Amount { get; set; }
        public string Description { get; set; }
        public int? ContractId { get; set; }
    }
}