using System.Collections.Generic;
using Contract.ViewModels;
using Contract.ViewModels.DAL;
using System.Linq.Expressions;
using System;
using Contract.Data.Models;

namespace Contract.Services.Interface
{
    public interface IContractDepartmentService : IService<ContractDepartmentViewModel, ContractDepartment>
    {

        IList<ContractDepartmentViewModel> GetByContract(int contractId);
        IList<ContractDepartmentViewModel> GetByDepartment(int departmentId);

    }
}