using Contract.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.ViewModels.DAL
{
    public class BranchViewModel
    {
        public int Id { get; set; }

        public BranchCode Code { get; set; }

        public string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }

    }
}
