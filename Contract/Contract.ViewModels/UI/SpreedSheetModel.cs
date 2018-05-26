using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Data.Models.Enums;

namespace Contract.ViewModels.UI
{
    public abstract class SpreedSheetModel
    {
        /// <summary>
        /// 
        /// </summary>
        public CategoryCode Category { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BranchCode Branch { get; set; }

        /// <summary>
        /// Наименование заказчика
        /// </summary>
        public string V2 { get; set; }

        /// <summary>
        /// Наименование объекта
        /// </summary>
        public string V3 { get; set; }

        /// <summary>
        /// ИНН заказчика
        /// </summary>
        public string V4 { get; set; }

        /// <summary>
        /// Расчетный счет
        /// </summary>
        public string V5 { get; set; }

        /// <summary>
        /// МФО
        /// </summary>
        public string V6 { get; set; }

        /// <summary>
        /// ОКОНХ
        /// </summary>
        public string V7 { get; set; }

        /// <summary>
        /// Контактные телефоны
        /// </summary>
        public string V8 { get; set; }

        /// <summary>
        /// Наименование банка заказчика
        /// </summary>
        public string V9 { get; set; }

        /// <summary>
        /// № договора
        /// </summary>
        public string V10 { get; set; }

        /// <summary>
        /// Дата договора
        /// </summary>
        public DateTime? V11 { get; set; }

        /// <summary>
        /// Сумма договора
        /// </summary>
        public double? V12 { get; set; }

        /// <summary>
        /// топография, без НДС
        /// </summary>
        public double? V13 { get; set; }

        /// <summary>
        /// геология, без НДС
        /// </summary>
        public double? V14 { get; set; }

        /// <summary>
        /// геодезия (в т.ч. перенос в натуру,вынос красных линий и другие), с НДС
        /// </summary>
        public double? V15 { get; set; }

        /// <summary>
        /// освидетельствование котлована (в т.ч. опр.плотн.грунта в обратной засыпке пазух фунд.и в подсыпке под полы), с НДС
        /// </summary>
        public double? V16 { get; set; }

        /// <summary>
        /// проектные работы идругие, с НДС
        /// </summary>
        public double? V17 { get; set; }

        /// <summary>
        /// техническое обследование, с НДС
        /// </summary>
        public double? V18 { get; set; }

        /// <summary>
        /// СМР, с НДС
        /// </summary>
        public double? V19 { get; set; }

        /// <summary>
        /// картографические работы, с НДС
        /// </summary>
        public double? V20 { get; set; }

        /// <summary>
        /// геоинформатика в т.ч. ГИС,ГГК и ЭГИТИ, без НДС
        /// </summary>
        public double? V21 { get; set; }

        /// <summary>
        /// выдача архивных материалов, с НДС
        /// </summary>
        public double? V22 { get; set; }

        /// <summary>
        /// выдача копии топопланов, с НДС
        /// </summary>
        public double? V23 { get; set; }

        /// <summary>
        /// прочие работы
        /// </summary>
        public double? V24 { get; set; }

        /// <summary>
        /// дата
        /// </summary>
        public DateTime? V25 { get; set; }

        /// <summary>
        /// сумма
        /// </summary>
        public double? V26 { get; set; }

        /// <summary>
        /// топография
        /// </summary>
        public DateTime? V27 { get; set; }

        /// <summary>
        /// геология
        /// </summary>
        public DateTime? V28 { get; set; }

        /// <summary>
        /// геодезия
        /// </summary>
        public DateTime? V29 { get; set; }

        /// <summary>
        /// проектная группа
        /// </summary>
        public DateTime? V30 { get; set; }

        /// <summary>
        /// проектная группа
        /// </summary>
        public DateTime? V31 { get; set; }

        /// <summary>
        /// Сроки сдачи работ по договору (после предоплаты)
        /// </summary>
        public DateTime? V32 { get; set; }

        /// <summary>
        /// Сроки сдачи работ по факту (после предоплаты)
        /// </summary>
        public DateTime? V33 { get; set; }

        /// <summary>
        /// № Акта
        /// </summary>
        public string V34 { get; set; }

        /// <summary>
        /// № счет-фактуры
        /// </summary>
        public string V35 { get; set; }

        /// <summary>
        /// № накладной
        /// </summary>
        public string V36 { get; set; }

        /// <summary>
        /// Дата накладной
        /// </summary>
        public DateTime? V37 { get; set; }

        /// <summary>
        /// Недостатки в комплектации экономического дела
        /// </summary>
        public string V38 { get; set; }

        /// <summary>
        /// Реализация готовой продукции - дата
        /// </summary>
        public DateTime? V39 { get; set; }

        /// <summary>
        /// Реализация готовой продукции - сумма
        /// </summary>
        public double? V40 { get; set; }

    }
}
