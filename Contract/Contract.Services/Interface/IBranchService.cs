using Contract.Data.Models;
using Contract.ViewModels.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Services.Interface
{
    public interface IBranchService : IService<BranchViewModel, Branch>
    {
        
    }
}
