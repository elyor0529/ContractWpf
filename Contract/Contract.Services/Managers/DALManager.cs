using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Contract.Core;
using Contract.Core.Extensions;
using Contract.Data.Models;
using Contract.Data.Models.Enums;
using Contract.Services.Interface;
using Contract.ViewModels.DAL;
using Contract.ViewModels.UI;

namespace Contract.Services.Managers
{
    public sealed class DalManager
    {
        public ImportStatus DoImportModel(ImportModel model)
        {

            ImportStatus status;

            try
            {
                /*
                     Service
                 */
                var organizationService = DiConfig.Resolve<IOrganizationService>();
                var contractService = DiConfig.Resolve<IContractService>();
                var contractPaymentService = DiConfig.Resolve<IContractWorkPaymentService>();
                var workTypeService = DiConfig.Resolve<IWorkTypeService>();
                var paymentService = DiConfig.Resolve<IPaymentService>();
                var contractDepartmentService = DiConfig.Resolve<IContractDepartmentService>();
                var departmentService = DiConfig.Resolve<IDepartmentService>();
                var actInvoiceService = DiConfig.Resolve<IActInvoiceService>();
                var nakladService = DiConfig.Resolve<INakladnoyService>();
                var categoryService = DiConfig.Resolve<ICategoryService>();
                var branchService = DiConfig.Resolve<IBranchService>();

                /*
                    Data
                */
                var workItems = new Dictionary<WorkTypeCode, double?>
                {
                    { WorkTypeCode.Col13, model.V13},
                    { WorkTypeCode.Col14, model.V14},
                    { WorkTypeCode.Col15, model.V15},
                    { WorkTypeCode.Col16, model.V16},
                    { WorkTypeCode.Col17, model.V17},
                    { WorkTypeCode.Col18, model.V18},
                    { WorkTypeCode.Col19, model.V19},
                    { WorkTypeCode.Col20, model.V20},
                    { WorkTypeCode.Col21, model.V21},
                    { WorkTypeCode.Col22, model.V22},
                    { WorkTypeCode.Col23, model.V23},
                    { WorkTypeCode.Col24, model.V24}
                };
                var departmentItems = new Dictionary<DepartmentCode, DateTime?>
                {
                    {DepartmentCode.Col27, model.V27},
                    {DepartmentCode.Col28, model.V28},
                    {DepartmentCode.Col29, model.V29},
                    {DepartmentCode.Col30, model.V30},
                    {DepartmentCode.Col31, model.V31}
                };

                /*
                    Model
                */
                var organizationModel = organizationService.Get(viewModel => viewModel.Inn.Equals(model.V4, StringComparison.InvariantCultureIgnoreCase));

                /*
                    Organization
                */
                if (organizationModel == null)
                {
                    organizationModel = new OrganizationViewModel
                    {
                        Inn = model.V4,
                        Name = model.V2,
                        BankName1 = model.V9,
                        AccountNumber = model.V5,
                        Mfo1 = model.V6,
                        Okohx = model.V7,
                        PhoneNumbers = model.V8
                    };
                    organizationModel.Id = organizationService.CreateOrUpdate(organizationModel);
                }
                else
                {
                    organizationModel.Name = model.V2;
                    organizationModel.BankName1 = model.V9;
                    organizationModel.AccountNumber = model.V5;
                    organizationModel.Mfo1 = model.V6;
                    organizationModel.Okohx = model.V7;
                    organizationModel.PhoneNumbers = model.V8;
                    organizationService.CreateOrUpdate(organizationModel);
                }

                /*
                      Contract    
                  */
                var contractModel = contractService.Get(contract => contract.OrganizationId == organizationModel.Id && contract.ContractNumber == model.V10);
                var categoryModel=categoryService.Get(a=>a.Code==model.Category);
                var branchModel=branchService.Get(a=>a.Code==model.Branch);

                if (contractModel == null)
                {

                    #region Create
                    
                    contractModel = new ContractViewModel
                    {
                        ContractAmount = model.V12,
                        ContractDate = model.V11,
                        ContractNumber = model.V10,
                        ObjectName = model.V3,
                        DeadlinesContract = model.V32,
                        DeadlinesFact = model.V33,
                        Limitation = model.V38,
                        CategoryId = categoryModel.Id,
                        BranchId = branchModel.Id,
                        OrganizationId = organizationModel.Id
                    };
                    contractModel.Id = contractService.CreateOrUpdate(contractModel);

                    /*
                       Contract & Payment
                   */
                    foreach (var workItem in workItems)
                    {
                        var workName = Enum.GetName(typeof(WorkTypeCode), workItem.Key);
                        var workType = workTypeService.Get(type => type.Name.Equals(workName, StringComparison.InvariantCultureIgnoreCase));

                        if (workType == null)
                            continue;

                        var contractPaymentModel = new ContractWorkPaymentViewModel
                        {
                            Amount = workItem.Value,
                            ContractId = contractModel.Id,
                            WorkTypeId = workType.Id
                        };

                        contractPaymentService.CreateOrUpdate(contractPaymentModel);
                    }

                    /*
                      Payment
                  */
                    var peymentModel = new PaymentViewModel
                    {
                        ContractId = contractModel.Id,
                        Amount = model.V26,
                        Date = model.V25
                    };
                    paymentService.CreateOrUpdate(peymentModel);

                    /*
                      Contract & Department
                  */
                    foreach (var departmentItem in departmentItems)
                    {
                        var departmentName = Enum.GetName(typeof(DepartmentCode), departmentItem.Key);
                        var department = departmentService.Get(department1 => department1.Name.Equals(departmentName, StringComparison.InvariantCultureIgnoreCase));

                        if (department == null)
                            continue;

                        var contractDepartmentModel = new ContractDepartmentViewModel
                        {
                            ContractId = contractModel.Id,
                            DepartmentId = department.Id,
                            Date = departmentItem.Value,
                            Code = departmentItem.Key
                        };
                        contractDepartmentService.CreateOrUpdate(contractDepartmentModel);
                    }

                    /*
                          Act Invoice
                      */
                    var actInvoiceModel = new ActInvoiceViewModel
                    {
                        ContractId = contractModel.Id,
                        Number = model.V34
                    };
                    actInvoiceService.CreateOrUpdate(actInvoiceModel);

                    /*
                      Nakladnoy
                  */
                    var nakladModel = new NakladnoyViewModel
                    {
                        ContractId = contractModel.Id,
                        Date = model.V37,
                        Number = model.V36
                    };
                    nakladService.CreateOrUpdate(nakladModel);

                    #endregion


                    status = ImportStatus.Created;

                }
                else
                {
                    #region Update

                    contractModel.ContractAmount = model.V12;
                    contractModel.ContractDate = model.V11;
                    contractModel.ObjectName = model.V3;
                    contractModel.DeadlinesContract = model.V32;
                    contractModel.DeadlinesFact = model.V33;
                    contractModel.Limitation = model.V38;
                    contractModel.CategoryId = categoryModel.Id;
                    contractModel.BranchId = branchModel.Id;
                    contractService.CreateOrUpdate(contractModel);

                    /*
                       Contract & Payment
                   */
                    foreach (var workItem in workItems)
                    {
                        var workName = Enum.GetName(typeof(WorkTypeCode), workItem.Key);
                        var workType = workTypeService.Get(type => type.Name.Equals(workName, StringComparison.InvariantCultureIgnoreCase));

                        if (workType == null)
                            continue;

                        var contractPaymentModel = contractPaymentService.Get(g => g.WorkTypeId == workType.Id && g.ContractId == contractModel.Id);

                        if (contractPaymentModel == null)
                            continue;

                        contractPaymentModel.Amount = workItem.Value;
                        contractPaymentService.CreateOrUpdate(contractPaymentModel);
                    }


                    /*
                      Contract & Department
                  */
                    foreach (var departmentItem in departmentItems)
                    {
                        var departmentName = Enum.GetName(typeof(DepartmentCode), departmentItem.Key);
                        var department = departmentService.Get(department1 => department1.Name.Equals(departmentName, StringComparison.InvariantCultureIgnoreCase));

                        if (department == null)
                            continue;

                        var contractDepartmentModel = contractDepartmentService.Get(g => g.ContractId == contractModel.Id && g.DepartmentId == department.Id);

                        if (contractDepartmentModel == null)
                            continue;

                        contractDepartmentModel.Date = departmentItem.Value;
                        contractDepartmentModel.Code = departmentItem.Key;

                        contractDepartmentService.CreateOrUpdate(contractDepartmentModel);
                    }

                    /*
                        Act Invoice
                    */
                    var actInvoiceModel = actInvoiceService.Get(invoice => invoice.ContractId == contractModel.Id && invoice.Number.Equals(model.V34, StringComparison.InvariantCultureIgnoreCase));

                    if (actInvoiceModel != null)
                    {
                        actInvoiceService.CreateOrUpdate(actInvoiceModel);
                    }

                    /*
                      Nakladnoy
                  */
                    var nakladModel = nakladService.Get(nakladnoy => nakladnoy.ContractId == contractModel.Id && nakladnoy.Number.Equals(model.V36, StringComparison.InvariantCultureIgnoreCase));

                    if (nakladModel != null)
                    {
                        nakladModel.Date = model.V37;
                        nakladService.CreateOrUpdate(nakladModel);
                    }

                    #endregion

                    status = ImportStatus.Updated;
                }

            }
            catch (Exception exp)
            {
                Trace.TraceError(exp.Message);

                status = ImportStatus.Failed;
            }

            return status;
        }

        public IList<ExportModel> GetExportModels(ContractFilterModel model)
        {
            var contractService = DiConfig.Resolve<IContractService>();
            var contracts = contractService.Repository.Include
                (
                    a => a.Organization,
                    a => a.ActInvoices,
                    a => a.Nakladnoys,
                    a => a.Payments,
                    a => a.ContractWorkPayments.Select(s => s.WorkType),
                    a => a.ContractWorkPayments.Select(s => s.Period),
                    a => a.ContractDepartments.Select(s => s.Department)
                );
            var exportModels = new List<ExportModel>();

            contractService.DoFilter(ref contracts, model);

            contracts = contracts.Where(w => w.Organization != null &&
                                                 w.ActInvoices.Any() &&
                                                 w.Nakladnoys.Any() &&
                                                 w.Payments.Any() &&
                                                 w.ContractDepartments.Any(a => a.Department != null) &&
                                                 w.ContractWorkPayments.Any(a => a.WorkType != null));
            foreach (var contract in contracts)
            {
                var exportModel = new ExportModel
                {
                    Id = contract.Id
                };

                /*
                    client
                */
                exportModel.V2 = contract.Organization.Name;
                exportModel.V4 = contract.Organization.Inn;
                exportModel.V5 = contract.Organization.AccountNumber;
                exportModel.V6 = contract.Organization.Mfo1;
                exportModel.V7 = contract.Organization.Okohx;
                exportModel.V8 = contract.Organization.PhoneNumbers;
                exportModel.V9 = contract.Organization.BankName1;

                /*
                    contract
                */
                exportModel.V3 = contract.ObjectName;
                exportModel.V10 = contract.ContractNumber;
                exportModel.V11 = contract.ContractDate;
                exportModel.V12 = contract.ContractAmount;
                exportModel.V38 = contract.Limitation;
                exportModel.V32 = contract.DeadlinesContract;
                exportModel.V33 = contract.DeadlinesFact;

                /*
                    work payment type
                */
                exportModel.V13 =
                    contract.ContractWorkPayments.FindOrDefault(f => f.WorkType.Code == WorkTypeCode.Col13).Amount;
                exportModel.V14 =
                    contract.ContractWorkPayments.FindOrDefault(f => f.WorkType.Code == WorkTypeCode.Col14).Amount;
                exportModel.V15 =
                    contract.ContractWorkPayments.FindOrDefault(f => f.WorkType.Code == WorkTypeCode.Col15).Amount;
                exportModel.V16 =
                    contract.ContractWorkPayments.FindOrDefault(f => f.WorkType.Code == WorkTypeCode.Col16).Amount;
                exportModel.V17 =
                    contract.ContractWorkPayments.FindOrDefault(f => f.WorkType.Code == WorkTypeCode.Col17).Amount;
                exportModel.V18 =
                    contract.ContractWorkPayments.FindOrDefault(f => f.WorkType.Code == WorkTypeCode.Col18).Amount;
                exportModel.V19 =
                    contract.ContractWorkPayments.FindOrDefault(f => f.WorkType.Code == WorkTypeCode.Col19).Amount;
                exportModel.V20 =
                    contract.ContractWorkPayments.FindOrDefault(f => f.WorkType.Code == WorkTypeCode.Col20).Amount;
                exportModel.V21 =
                    contract.ContractWorkPayments.FindOrDefault(f => f.WorkType.Code == WorkTypeCode.Col21).Amount;
                exportModel.V22 =
                    contract.ContractWorkPayments.FindOrDefault(f => f.WorkType.Code == WorkTypeCode.Col22).Amount;
                exportModel.V23 =
                    contract.ContractWorkPayments.FindOrDefault(f => f.WorkType.Code == WorkTypeCode.Col23).Amount;
                exportModel.V24 =
                    contract.ContractWorkPayments.FindOrDefault(f => f.WorkType.Code == WorkTypeCode.Col24).Amount;

                /*
                    contract department
                */
                exportModel.V27 =
                    contract.ContractDepartments.FindOrDefault(f => f.Department.Code == DepartmentCode.Col27).Date;
                exportModel.V28 =
                    contract.ContractDepartments.FindOrDefault(f => f.Department.Code == DepartmentCode.Col28).Date;
                exportModel.V29 =
                    contract.ContractDepartments.FindOrDefault(f => f.Department.Code == DepartmentCode.Col29).Date;
                exportModel.V30 =
                    contract.ContractDepartments.FindOrDefault(f => f.Department.Code == DepartmentCode.Col30).Date;
                exportModel.V31 =
                    contract.ContractDepartments.FindOrDefault(f => f.Department.Code == DepartmentCode.Col31).Date;

                /*
                    act invoice
                */
                exportModel.V34 = contract.ActInvoices.FindOrDefault().Number;

                /*
                    contract payment
                */
                exportModel.V25 = contract.Payments.FindOrDefault().Date;
                exportModel.V26 = contract.Payments.FindOrDefault().Amount;

                /*
                    nakladnoy    
                */
                exportModel.V36 = contract.Nakladnoys.FindOrDefault().Number;
                exportModel.V37 = contract.Nakladnoys.FindOrDefault().Date;

                exportModels.Add(exportModel);
            }

            return exportModels;
        }


    }
}