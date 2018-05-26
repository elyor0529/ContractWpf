using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Data.Models.Base;

namespace Contract.Data.Models
{
    [Table("Periods")]
    public class Period : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

    }
}
