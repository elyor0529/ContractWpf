using Contract.Core;
using Contract.Core.Extensions;
using Contract.Services.Interface;
using Contract.Services.Properties;
using Contract.ViewModels.DAL;
using Novacode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Contract.Services.Helpers {
    public static class WordHelper {

        public static string ContractExport(int contractId) {
            var contractService = DiConfig.Resolve<IContractService>();
            var organizationService = DiConfig.Resolve<IOrganizationService>();
            var workPaymentService = DiConfig.Resolve<IContractWorkPaymentService>();
            var workTypeService = DiConfig.Resolve<IWorkTypeService>();
            var actInvoiceService = DiConfig.Resolve<IActInvoiceService>();
            var categoryService = DiConfig.Resolve<ICategoryService>();
            var branchService = DiConfig.Resolve<IBranchService>();
            var contractModel = contractService.GetById(contractId);
            var actInvoiceModel = actInvoiceService.Get(a => a.ContractId == contractId);
            var organizationModel = organizationService.GetById(contractModel.OrganizationId.GetValueOrDefault());
            var workTypes = workPaymentService.GetAll(a => a.ContractId == contractId);
            var categoryModel = categoryService.GetById(contractModel.CategoryId ?? 0);
            var branchModel = branchService.GetById(contractModel.BranchId ?? 0);
            var resourceKey = String.Format("contract_template{0}_{1}",(int) branchModel.Code, (int)categoryModel.Code);
            var resource = Resources.ResourceManager.GetObject(resourceKey);

            MemoryStream file = null;

            if (resource is byte[]) {
                file = new MemoryStream((byte[])resource);
            } else {
                throw new InvalidCastException("The specified resource is not a binary resource.");
            }

            var filePath = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetRandomFileName(), "docx"));

            using (var document = DocX.Load(file)) {
                document.ReplaceText(ExportConfig.KEYS.CONTRACT_NUMBER, contractModel.ContractNumber??"", false, RegexOptions.IgnoreCase);
                document.ReplaceText(ExportConfig.KEYS.OBJECT_NAME, contractModel.ObjectName??"", false, RegexOptions.IgnoreCase);
                document.ReplaceText(ExportConfig.KEYS.ORGANIZATION_NAME, organizationModel.Name??"", false, RegexOptions.IgnoreCase);
                document.ReplaceText(ExportConfig.KEYS.CONTRACT_DATE, contractModel.ContractDate.GetValueOrDefault(DateTime.Now).ToShortDateString(), false, RegexOptions.IgnoreCase);
                document.ReplaceText(ExportConfig.KEYS.CONTRACT_TOTAL_PRICE, contractModel.ContractAmount.GetValueOrDefault(0).ToString("N"), false, RegexOptions.IgnoreCase);
                document.ReplaceText(ExportConfig.KEYS.CONTRACT_TOTAL_PRICE_STR, contractModel.ContractAmount.GetValueOrDefault(0).Speach(), false, RegexOptions.IgnoreCase);
                document.ReplaceText(ExportConfig.KEYS.ORGANIZATION_ACCOUNT_NUMBER, organizationModel.AccountNumber??"", false, RegexOptions.IgnoreCase);
                document.ReplaceText(ExportConfig.KEYS.ORGANIZATION_ADDRESS, organizationModel.LegalAddress ?? "", false, RegexOptions.IgnoreCase);
                document.ReplaceText(ExportConfig.KEYS.ORGANIZATION_INN, organizationModel.Inn??"", false, RegexOptions.IgnoreCase);
                document.ReplaceText(ExportConfig.KEYS.ORGANIZATION_MFO1, organizationModel.Mfo1??"", false, RegexOptions.IgnoreCase);
                document.ReplaceText(ExportConfig.KEYS.ORGANIZATION_OKOHX, organizationModel.Okohx??"", false, RegexOptions.IgnoreCase);
                document.ReplaceText(ExportConfig.KEYS.ORGANIZATION_PHONE_NUMBER, organizationModel.PhoneNumbers??"", false, RegexOptions.IgnoreCase);
                document.ReplaceText(ExportConfig.KEYS.CONTRACT_WORK_TYPE, String.Join(",", workTypes.Select(s => workTypeService.GetById(s.WorkTypeId.GetValueOrDefault(0)).Descrption)), false, RegexOptions.IgnoreCase);
                document.ReplaceText(ExportConfig.KEYS.CONTRACT_WORK_TOTAL_AMOUNT, workTypes.Sum(s => s.Amount.GetValueOrDefault(0)).ToString("N"), false, RegexOptions.IgnoreCase);
                document.ReplaceText(ExportConfig.KEYS.CONTRACT_WORK_TOTAL_AMOUNT_STR, workTypes.Sum(s => s.Amount).GetValueOrDefault(0).Speach(), false, RegexOptions.IgnoreCase);
                document.ReplaceText(ExportConfig.KEYS.ACTINVOICE_DATE, DateTime.Now.ToShortDateString(), false, RegexOptions.IgnoreCase);
                document.ReplaceText(ExportConfig.KEYS.ACTINVOICE_NUMBER, actInvoiceModel.Number??"", false, RegexOptions.IgnoreCase);

                document.SaveAs(filePath);
            }

            return filePath;
        }

    }
}
