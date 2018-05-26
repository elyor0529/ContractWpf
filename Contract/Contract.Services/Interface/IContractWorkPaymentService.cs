using System.Collections.Generic;
using Contract.ViewModels;
using Contract.ViewModels.DAL;
using System.Linq.Expressions;
using System;
using Contract.Data.Models;

namespace Contract.Services.Interface
{
    public interface IContractWorkPaymentService : IService<ContractWorkPaymentViewModel, ContractWorkPayment>
    {

        IList<ContractWorkPaymentViewModel> GetByContract(int contractId);
        IList<ContractWorkPaymentViewModel> GetByWorkType(int worktypeId);

    }
}