using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Contract.Data.Models;
using Contract.Data.Models.Enums;

namespace Contract.ViewModels.UI
{

    public enum ImportStatus
    {
        Created,
        Updated,
        Failed
    }

    public class ImportModel : SpreedSheetModel
    {

        /// <summary>
        /// № п/п
        /// </summary>
        public int? V1 { get; set; }

        public ImportStatus Status { get; set; }
         
    }

    public class ImportDataModel
    {
        public IDictionary<CategoryCode, IList<ImportModel>> Items { get; set; }

        public IDictionary<CategoryCode, int> PageRows { get; set; }

        public int TotalRows
        {
            get
            {
                return Items.Sum(s => s.Value.Count);
            }
        }

        public int TotalPages
        {
            get
            {
                return Items.Keys.Count;
            }
        }

        public ImportDataModel()
        {
            Items = new Dictionary<CategoryCode, IList<ImportModel>>();
            PageRows = new Dictionary<CategoryCode, int>();
        }

    }

}