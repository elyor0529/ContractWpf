using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Contract.Data.Models;
using Contract.ViewModels;
using Contract.ViewModels.DAL;

namespace Contract.Services.Interface
{
    public interface INakladnoyService : IService<NakladnoyViewModel, Nakladnoy>
    {
        IList<NakladnoyViewModel> GetByContract(int contractId);

    }
}