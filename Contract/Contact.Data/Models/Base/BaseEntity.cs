using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contract.Data.Models.Base
{
    public abstract class BaseEntity : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]
        public DateTime? ModefiedDate { get; set; }

        public bool? IsDeleted { get; set; }
    }
}