using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contract.Data.Models.Base;
using Contract.Data.Models.Enums;

namespace Contract.Data.Models
{
    [Table("ContractDepartments")]
    public class ContractDepartment : BaseEntity
    {

        public int? ContractId { get; set; }

        [ForeignKey("ContractId")]
        public Contract Contract { get; set; }

        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Date { get; set; }
         
    }
}