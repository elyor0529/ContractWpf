using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Contract.Data.Models.Enums;
using Contract.ViewModels.Enums;

namespace Contract.ViewModels.UI
{
    public class ContractFilterModel
    {
        public ReportType? Type { get; set; }

        public CategoryCode? Code { get; set; }
         
        [DisplayName("Наименование(заказчика,обьекта)")]
        public string Nick { get; set; }

        [DisplayName("Дата начала")] 
        public DateTime? StartDate { get; set; }

        [DisplayName("Дата окончания")] 
        public DateTime? EndDate { get; set; }

        [DisplayName("Номер договора")]
        public string Number { get; set; }

        public dynamic Models { get; set; }
    }
}