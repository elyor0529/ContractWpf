using System.Collections.Generic;
using Contract.Data.Models.Base;
using Contract.Data.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contract.Data.Models
{
    [Table("Departments")]
    public class Department : BaseEntity
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public DepartmentCode Code { get; set; } 

        public ICollection<ContractDepartment> ContractDepartments { get; set; }

        public Department()
        {
            ContractDepartments = new List<ContractDepartment>();
        }

    }
}