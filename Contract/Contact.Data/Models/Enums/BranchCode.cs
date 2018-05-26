using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Data.Models.Enums
{
    public enum BranchCode
    {
        [Description("Центральное производство")]
        Brach1 = 10,

        [Description("Джизакский филиал")]
        Brach2 = 11,

        [Description("Наманганский филиал")]
        Brach3 = 12,

        [Description("Андижанский филиал")]
        Brach4 = 14,

        [Description("Бухаранский филиал")]
        Brach5 = 17,

        [Description("Кашкадарьинский филиал")]
        Brach6 = 18,

        [Description("Хорезмский филиал")]
        Brach7 = 19,

        [Description("Сурхандарьинский филиал")]
        Brach8 = 20,

        [Description("Навоийский филиал")]
        Brach9 = 21,

        [Description("Сырдарьинский филиал")]
        Brach10 = 22,

        [Description("Ферганский филиал")]
        Brach11 = 25,

        [Description("Нукусский филиал")]
        Brach12 = 26,

        [Description("Самаркандский филиал")]
        Brach13 = 27,

        [Description("Ташкентский городской филиал")]
        Brach14 = 28
    }
}
