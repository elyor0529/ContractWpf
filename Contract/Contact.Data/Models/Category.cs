using System.ComponentModel.DataAnnotations.Schema;
using Contract.Data.Models.Base;
using Contract.Data.Models.Enums;

namespace Contract.Data.Models
{
    [Table("Categories")]
    public class Category : BaseEntity
    {
        public CategoryCode Code { get; set; }

        public int? Counter { get; set; } 

        public string Note { get; set; }

    }
}