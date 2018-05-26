using System.ComponentModel.DataAnnotations.Schema;
using Contract.Data.Models.Base;
using Contract.Data.Models.Enums;

namespace Contract.Data.Models
{
    [Table("Branches")]
    public class Branch : BaseEntity
    {

        public BranchCode Code { get; set; }

        public string Title { get; set; }

    }
}