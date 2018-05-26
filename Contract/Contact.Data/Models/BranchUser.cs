using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Data.Models.Base;

namespace Contract.Data.Models
{
    [Table("BranchUsers")]
    public class BranchUser : BaseEntity
    {

        public int? BranchId { get; set; }

        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }

        public string UserName { get; set; }

    }
}
