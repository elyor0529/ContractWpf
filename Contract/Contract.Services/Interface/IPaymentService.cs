using System.Collections.Generic;
using Contract.Data.Models;
using Contract.ViewModels;
using Contract.ViewModels.DAL;

namespace Contract.Services.Interface
{
    public interface IPaymentService:IService<PaymentViewModel,Payment>
    { 
        List<PaymentViewModel> GetByContract(int contractId); 
    }
}