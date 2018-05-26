using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Data.Models;
using Contract.ViewModels.DAL;

namespace Contract.Services.Interface
{
    public interface IPeriodService : IService<PeriodViewModel, Period>
    {
    }
}
