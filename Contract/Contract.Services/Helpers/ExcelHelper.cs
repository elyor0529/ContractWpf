using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Contract.Core;
using Contract.Services.Interface;
using Contract.ViewModels;
using Contract.ViewModels.UI;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Data;
using Contract.Core.Helpers;
using Contract.Data.Models;
using Contract.Data.Models.Enums;
using Contract.Services.Managers;
using Contract.ViewModels.UI.Reports;
using Excel;
using Contract.Services.Properties;

namespace Contract.Services.Helpers
{
    public static class ExcelHelper
    {

        private const int StartRowIndex = 11;

        private static void FitToColumns(IXLWorksheet ws, int count)
        {

            //wrap text
            ws.Column(2).Width = 20;
            ws.Column(3).Width = 30;
            for (var i = 0; i < count; i++)
            {
                ws.Cell(4 + i, 2).Style.Alignment.WrapText = true;
                ws.Cell(4 + i, 2).Style.Alignment.ShrinkToFit = true;

                ws.Cell(4 + i, 3).Style.Alignment.WrapText = true;
                ws.Cell(4 + i, 3).Style.Alignment.ShrinkToFit = true;
            }

        }

        public static ImportDataModel GetImportModels(string path)
        {

            try
            {
                var model = new ImportDataModel();
                var stream = File.OpenRead(path);

                IExcelDataReader excelReader = null;

                //Reading from a binary Excel file ('97-2003 format; *.xls)
                if (Path.GetExtension(path).Equals(".xls", StringComparison.InvariantCultureIgnoreCase))
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);

                //Reading from a OpenXml Excel file (2007 format; *.xlsx)
                if (Path.GetExtension(path).Equals(".xlsx", StringComparison.InvariantCultureIgnoreCase))
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                if (excelReader == null)
                    return model;

                var pageIndex = 0;

                do
                {
                    pageIndex++;

                    var items = new List<ImportModel>();
                    var category = (CategoryCode)pageIndex;
                    var rowIndex = 0;

                    while (excelReader.Read())
                    {
                        rowIndex++;

                        if (rowIndex < StartRowIndex)
                            continue;

                        if (excelReader.IsDBNull(3))
                            continue;

                        var item = new ImportModel();

                        item.V1 = excelReader.IsDBNull(0) ? (int?)null : excelReader.GetInt32(0);
                        item.V2 = excelReader.GetString(1);
                        item.V3 = excelReader.GetString(2);
                        item.V4 = excelReader.GetString(3);
                        item.V5 = excelReader.GetString(4);
                        item.V6 = excelReader.GetString(5);
                        item.V7 = excelReader.GetString(6);
                        item.V8 = excelReader.GetString(7);
                        item.V9 = excelReader.GetString(8);
                        item.V10 = excelReader.GetString(9);
                        item.V11 = excelReader.IsDBNull(10) ? (DateTime?)null : excelReader.GetDateTime(10);
                        item.V12 = excelReader.IsDBNull(11) ? (double?)null : excelReader.GetDouble(11);
                        item.V13 = excelReader.IsDBNull(12) ? (double?)null : excelReader.GetDouble(12);
                        item.V14 = excelReader.IsDBNull(13) ? (double?)null : excelReader.GetDouble(13);
                        item.V15 = excelReader.IsDBNull(14) ? (double?)null : excelReader.GetDouble(14);
                        item.V16 = excelReader.IsDBNull(15) ? (double?)null : excelReader.GetDouble(15);
                        item.V17 = excelReader.IsDBNull(16) ? (double?)null : excelReader.GetDouble(16);
                        item.V18 = excelReader.IsDBNull(17) ? (double?)null : excelReader.GetDouble(17);
                        item.V19 = excelReader.IsDBNull(18) ? (double?)null : excelReader.GetDouble(18);
                        item.V20 = excelReader.IsDBNull(19) ? (double?)null : excelReader.GetDouble(19);
                        item.V21 = excelReader.IsDBNull(20) ? (double?)null : excelReader.GetDouble(20);
                        item.V22 = excelReader.IsDBNull(21) ? (double?)null : excelReader.GetDouble(21);
                        item.V23 = excelReader.IsDBNull(22) ? (double?)null : excelReader.GetDouble(22);
                        item.V24 = excelReader.IsDBNull(23) ? (double?)null : excelReader.GetDouble(23);
                        item.V25 = excelReader.IsDBNull(24) ? (DateTime?)null : excelReader.GetDateTime(24);
                        item.V26 = excelReader.IsDBNull(25) ? (double?)null : excelReader.GetDouble(25);
                        item.V27 = excelReader.IsDBNull(26) ? (DateTime?)null : excelReader.GetDateTime(26);
                        item.V28 = excelReader.IsDBNull(27) ? (DateTime?)null : excelReader.GetDateTime(27);
                        item.V29 = excelReader.IsDBNull(28) ? (DateTime?)null : excelReader.GetDateTime(28);
                        item.V30 = excelReader.IsDBNull(29) ? (DateTime?)null : excelReader.GetDateTime(29);
                        item.V31 = excelReader.IsDBNull(30) ? (DateTime?)null : excelReader.GetDateTime(30);
                        item.V32 = excelReader.IsDBNull(31) ? (DateTime?)null : excelReader.GetDateTime(31);
                        item.V33 = excelReader.IsDBNull(32) ? (DateTime?)null : excelReader.GetDateTime(32);
                        item.V34 = excelReader.GetString(33);
                        item.V35 = excelReader.GetString(34);
                        item.V36 = excelReader.GetString(35);
                        item.V37 = excelReader.IsDBNull(36) ? (DateTime?)null : excelReader.GetDateTime(36);
                        item.V38 = excelReader.GetString(37);
                        item.V39 = excelReader.IsDBNull(38) ? (DateTime?)null : excelReader.GetDateTime(38);
                        item.V40 = excelReader.IsDBNull(39) ? (double?)null : excelReader.GetDouble(39);


                        items.Add(item);
                    }

                    model.Items.Add(category, items);
                    model.PageRows.Add(category, rowIndex);

                } while (excelReader.NextResult());

                excelReader.Close();

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string DoExport(ContractFilterModel model, int startRow, int sizeRow)
        {
            try
            {
                var manager = new DalManager();
                var contractModels = manager.GetExportModels(model);
                var file = new MemoryStream(Resources.template0);
                var wb = new XLWorkbook(file);
                var ws = wb.Worksheets.Worksheet(1);
                var filePath = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetRandomFileName(), "xlsx"));
                var topRowIndex = 4;

                for (var i = startRow; (i < startRow + sizeRow) && (i < contractModels.Count); i++)
                {
                    ws.Cell(topRowIndex + i, 1).Value = i + 1;
                    ws.Cell(topRowIndex + i, 2).Value = contractModels[i].V2;
                    ws.Cell(topRowIndex + i, 3).Value = contractModels[i].V3;
                    ws.Cell(topRowIndex + i, 4).Value = contractModels[i].V4;
                    ws.Cell(topRowIndex + i, 5).SetValue(contractModels[i].V5);
                    ws.Cell(topRowIndex + i, 6).Value = contractModels[i].V6;
                    ws.Cell(topRowIndex + i, 7).Value = contractModels[i].V7;
                    ws.Cell(topRowIndex + i, 8).Value = contractModels[i].V8;
                    ws.Cell(topRowIndex + i, 9).Value = contractModels[i].V9;
                    ws.Cell(topRowIndex + i, 10).SetValue(contractModels[i].V10);

                    ws.Cell(topRowIndex + i, 11).Value = contractModels[i].V11;
                    ws.Cell(topRowIndex + i, 11).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(topRowIndex + i, 12).Value = contractModels[i].V12;
                    ws.Cell(topRowIndex + i, 12).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 13).Value = contractModels[i].V13;
                    ws.Cell(topRowIndex + i, 13).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 14).Value = contractModels[i].V14;
                    ws.Cell(topRowIndex + i, 14).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 15).Value = contractModels[i].V15;
                    ws.Cell(topRowIndex + i, 15).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 16).Value = contractModels[i].V16;
                    ws.Cell(topRowIndex + i, 16).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 17).Value = contractModels[i].V17;
                    ws.Cell(topRowIndex + i, 17).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 18).Value = contractModels[i].V18;
                    ws.Cell(topRowIndex + i, 18).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 19).Value = contractModels[i].V19;
                    ws.Cell(topRowIndex + i, 19).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 20).Value = contractModels[i].V20;
                    ws.Cell(topRowIndex + i, 20).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 21).Value = contractModels[i].V21;
                    ws.Cell(topRowIndex + i, 21).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 22).Value = contractModels[i].V22;
                    ws.Cell(topRowIndex + i, 22).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 23).Value = contractModels[i].V23;
                    ws.Cell(topRowIndex + i, 23).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 24).Value = contractModels[i].V24;
                    ws.Cell(topRowIndex + i, 24).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 25).Value = contractModels[i].V25;
                    ws.Cell(topRowIndex + i, 25).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(topRowIndex + i, 26).Value = contractModels[i].V26;
                    ws.Cell(topRowIndex + i, 26).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 27).Value = contractModels[i].V27;
                    ws.Cell(topRowIndex + i, 27).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(topRowIndex + i, 28).Value = contractModels[i].V28;
                    ws.Cell(topRowIndex + i, 28).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(topRowIndex + i, 29).Value = contractModels[i].V29;
                    ws.Cell(topRowIndex + i, 29).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(topRowIndex + i, 30).Value = contractModels[i].V30;
                    ws.Cell(topRowIndex + i, 30).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(topRowIndex + i, 31).Value = contractModels[i].V31;
                    ws.Cell(topRowIndex + i, 31).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(topRowIndex + i, 32).Value = contractModels[i].V32;
                    ws.Cell(topRowIndex + i, 32).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(topRowIndex + i, 33).Value = contractModels[i].V33;
                    ws.Cell(topRowIndex + i, 33).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(topRowIndex + i, 34).SetValue(contractModels[i].V34);

                    ws.Cell(topRowIndex + i, 35).SetValue(contractModels[i].V35);

                    ws.Cell(topRowIndex + i, 36).SetValue(contractModels[i].V36);

                    ws.Cell(topRowIndex + i, 37).Value = contractModels[i].V37;
                    ws.Cell(topRowIndex + i, 37).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(topRowIndex + i, 38).Value = contractModels[i].V38;

                    ws.Cell(topRowIndex + i, 39).Value = contractModels[i].V39;
                    ws.Cell(topRowIndex + i, 39).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(topRowIndex + i, 40).Value = contractModels[i].V40;
                    ws.Cell(topRowIndex + i, 40).Style.NumberFormat.Format = "#,##0.00";

                }

                FitToColumns(ws, contractModels.Count);

                //save
                wb.SaveAs(filePath);

                return filePath;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string DoExportReport1(ContractFilterModel model)
        {
            try
            {
                var contractService = DiConfig.Resolve<IContractService>();
                var contractModels = contractService.Report1Filter(model);
                var file = new MemoryStream(Resources.template1);
                var wb = new XLWorkbook(file);
                var ws = wb.Worksheets.Worksheet(1);
                var filePath = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetRandomFileName(), "xlsx"));
                var topRowIndex = 8;

                //rows 
                ws.Range(topRowIndex + 1, 1, contractModels.Count + topRowIndex, 10)
                    .InsertRowsAbove(contractModels.Count, true);

                ws.Cell(5, 8).Value = DateTime.Now;
                ws.Cell(5, 8).Style.DateFormat.Format = "dd/MM/yyyy";

                for (var i = 0; i < contractModels.Count; i++)
                {
                    ws.Cell(topRowIndex + i, 1).Value = i + 1;
                    ws.Cell(topRowIndex + i, 2).Value = contractModels[i].Client;
                    ws.Cell(topRowIndex + i, 3).Value = contractModels[i].Object;
                    ws.Cell(topRowIndex + i, 4).SetValue(contractModels[i].Number);

                    ws.Cell(topRowIndex + i, 5).Value = contractModels[i].Date;
                    ws.Cell(topRowIndex + i, 5).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(topRowIndex + i, 6).Value = contractModels[i].Amount;
                    ws.Cell(topRowIndex + i, 6).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 7).Value = contractModels[i].Paid;
                    ws.Cell(topRowIndex + i, 7).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 8).Value = contractModels[i].Debt;
                    ws.Cell(topRowIndex + i, 8).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 9).Value = contractModels[i].Comment;
                    ws.Cell(topRowIndex + i, 10).Value = String.Empty;
                }

                FitToColumns(ws, contractModels.Count);

                //save
                wb.SaveAs(filePath);

                return filePath;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string DoExportReport2(ContractFilterModel model)
        {
            try
            {
                var contractService = DiConfig.Resolve<IContractService>();
                var contractModels = contractService.Report2Filter(model);
                var file = new MemoryStream(Resources.template2);
                var wb = new XLWorkbook(file);
                var ws = wb.Worksheets.Worksheet(1);
                var filePath = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetRandomFileName(), "xlsx"));
                var topRowIndex = 9;

                //rows 
                ws.Range(topRowIndex + 1, 1, contractModels.Count + topRowIndex, 16)
                    .InsertRowsAbove(contractModels.Count, true);

                ws.Cell(5, 14).Value = DateTime.Now;
                ws.Cell(5, 14).Style.DateFormat.Format = "dd/MM/yyyy";

                for (var i = 0; i < contractModels.Count; i++)
                {
                    ws.Cell(topRowIndex + i, 1).Value = i + 1;
                    ws.Cell(topRowIndex + i, 2).Value = contractModels[i].Client;
                    ws.Cell(topRowIndex + i, 3).Value = contractModels[i].Object;
                    ws.Cell(topRowIndex + i, 4).SetValue(contractModels[i].Number);

                    ws.Cell(topRowIndex + i, 5).Value = contractModels[i].Date;
                    ws.Cell(topRowIndex + i, 5).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(topRowIndex + i, 6).Value = contractModels[i].Col13;
                    ws.Cell(topRowIndex + i, 6).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 7).Value = contractModels[i].Col14;
                    ws.Cell(topRowIndex + i, 7).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 8).Value = contractModels[i].Col15;
                    ws.Cell(topRowIndex + i, 8).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 9).Value = contractModels[i].Col16;
                    ws.Cell(topRowIndex + i, 9).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 10).Value = contractModels[i].Col17;
                    ws.Cell(topRowIndex + i, 10).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 11).Value = contractModels[i].Col18;
                    ws.Cell(topRowIndex + i, 11).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 11).Value = contractModels[i].Col18;
                    ws.Cell(topRowIndex + i, 11).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 12).Value = contractModels[i].Col19;
                    ws.Cell(topRowIndex + i, 12).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 13).Value = contractModels[i].Col24;
                    ws.Cell(topRowIndex + i, 13).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 14).Value = contractModels[i].Amount;
                    ws.Cell(topRowIndex + i, 14).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 15).Value = contractModels[i].Paid;
                    ws.Cell(topRowIndex + i, 15).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 16).Value = contractModels[i].Residue;
                    ws.Cell(topRowIndex + i, 16).Style.NumberFormat.Format = "#,##0.00";
                }

                FitToColumns(ws, contractModels.Count);

                //save
                wb.SaveAs(filePath);


                return filePath;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string DoExportReport3(ContractFilterModel model)
        {
            try
            {
                var contractService = DiConfig.Resolve<IContractService>();
                var contractModels = contractService.Report3Filter(model);
                var file = new MemoryStream(Resources.template3);
                var wb = new XLWorkbook(file);
                var ws = wb.Worksheets.Worksheet(1);
                var filePath = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetRandomFileName(), "xlsx"));
                var topRowIndex = 9;

                //rows
                ws.Range(topRowIndex + 1, 1, contractModels.Count + topRowIndex, 10)
                   .InsertRowsAbove(contractModels.Count, true);

                ws.Cell(5, 8).Value = DateTime.Now;
                ws.Cell(5, 8).Style.DateFormat.Format = "dd/MM/yyyy";

                for (var i = 0; i < contractModels.Count; i++)
                {
                    ws.Cell(topRowIndex + i, 1).Value = i + 1;
                    ws.Cell(topRowIndex + i, 2).Value = contractModels[i].Client;
                    ws.Cell(topRowIndex + i, 3).Value = contractModels[i].Object;
                    ws.Cell(topRowIndex + i, 4).SetValue(contractModels[i].Number);

                    ws.Cell(topRowIndex + i, 5).Value = contractModels[i].Date;
                    ws.Cell(topRowIndex + i, 5).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(topRowIndex + i, 6).Value = contractModels[i].Amount;
                    ws.Cell(topRowIndex + i, 6).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 7).Value = contractModels[i].Paid;
                    ws.Cell(topRowIndex + i, 7).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 8).Value = contractModels[i].Debt;
                    ws.Cell(topRowIndex + i, 8).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 9).Value = contractModels[i].ContractStatus;

                    ws.Cell(topRowIndex + i, 10).Value = contractModels[i].ActInvoiceNumber != null ? "Yes" : "No";

                }

                FitToColumns(ws, contractModels.Count);

                //save
                wb.SaveAs(filePath);

                return filePath;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string DoExportReport4(ContractFilterModel model)
        {
            try
            {
                var contractService = DiConfig.Resolve<IContractService>();
                var contractModels = contractService.Report4Filter(model);
                var file = new MemoryStream(Resources.template4);
                var wb = new XLWorkbook(file);
                var ws = wb.Worksheets.Worksheet(1);
                var filePath = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetRandomFileName(), "xlsx"));
                var topRowIndex = 9;

                //rows
                ws.Range(topRowIndex + 1, 1, contractModels.Count + topRowIndex, 10)
                   .InsertRowsAbove(contractModels.Count, true);

                ws.Cell(5, 8).Value = DateTime.Now;
                ws.Cell(5, 8).Style.DateFormat.Format = "dd/MM/yyyy";

                for (var i = 0; i < contractModels.Count; i++)
                {
                    ws.Cell(topRowIndex + i, 1).Value = i + 1;
                    ws.Cell(topRowIndex + i, 2).Value = contractModels[i].Client;
                    ws.Cell(topRowIndex + i, 3).Value = contractModels[i].Object;
                    ws.Cell(topRowIndex + i, 4).SetValue(contractModels[i].Number);

                    ws.Cell(topRowIndex + i, 5).Value = contractModels[i].Col27;
                    ws.Cell(topRowIndex + i, 5).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(topRowIndex + i, 6).Value = contractModels[i].Col28;
                    ws.Cell(topRowIndex + i, 6).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(topRowIndex + i, 7).Value = contractModels[i].Col31;
                    ws.Cell(topRowIndex + i, 7).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(topRowIndex + i, 8).Value = contractModels[i].Date;
                    ws.Cell(topRowIndex + i, 8).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(topRowIndex + i, 9).Value = contractModels[i].Amount;
                    ws.Cell(topRowIndex + i, 9).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 10).Value = contractModels[i].Paid;
                    ws.Cell(topRowIndex + i, 10).Style.NumberFormat.Format = "#,##0.00";

                }

                FitToColumns(ws, contractModels.Count);

                //save
                wb.SaveAs(filePath);

                return filePath;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string DoExportReport5(ContractFilterModel model)
        {
            try
            {
                var contractService = DiConfig.Resolve<IContractService>();
                var contractModels = contractService.Report5Filter(model);
                var file = new MemoryStream(Resources.template5);
                var wb = new XLWorkbook(file);
                var ws = wb.Worksheets.Worksheet(1);
                var filePath = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetRandomFileName(), "xlsx"));
                var topRowIndex = 9;

                //rows
                ws.Range(topRowIndex + 1, 1, contractModels.Count + topRowIndex, 11)
                   .InsertRowsAbove(contractModels.Count, true);

                ws.Cell(5, 9).Value = DateTime.Now;
                ws.Cell(5, 9).Style.DateFormat.Format = "dd/MM/yyyy";

                for (var i = 0; i < contractModels.Count; i++)
                {
                    ws.Cell(topRowIndex + i, 1).Value = i + 1;
                    ws.Cell(topRowIndex + i, 2).Value = contractModels[i].Client;
                    ws.Cell(topRowIndex + i, 3).Value = contractModels[i].Object;
                    ws.Cell(topRowIndex + i, 4).SetValue(contractModels[i].Number);

                    ws.Cell(topRowIndex + i, 5).Value = contractModels[i].Amount;
                    ws.Cell(topRowIndex + i, 5).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 6).Value = contractModels[i].OwnPrice;
                    ws.Cell(topRowIndex + i, 6).Style.NumberFormat.Format = "#,##0.00";

                    ws.Cell(topRowIndex + i, 7).Value = contractModels[i].NDSPrice;
                    ws.Cell(topRowIndex + i, 7).Style.NumberFormat.Format = "#,##0.00";

                }

                FitToColumns(ws, contractModels.Count);

                //save
                wb.SaveAs(filePath);

                return filePath;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
         
    }
}
