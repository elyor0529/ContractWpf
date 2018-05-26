using Contract.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.ViewModels.DAL
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public CategoryCode Code { get; set; }

        public string Note { get; set; }

        public int? Counter { get; set; }

        public override string ToString()
        {
            return Note;
        }

    }
}
