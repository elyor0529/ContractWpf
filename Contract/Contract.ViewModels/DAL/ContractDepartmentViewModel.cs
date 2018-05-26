using System;
using Contract.Data.Models;
using Contract.Data.Models.Enums;

namespace Contract.ViewModels.DAL
{
    public class ContractDepartmentViewModel
    {
        public int Id { get; set; }
        public int? ContractId { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? Date { get; set; }
        public DepartmentCode Code { get; set; }
    }
}