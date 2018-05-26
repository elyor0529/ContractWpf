using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Contract.ViewModels.Annotations;

namespace Contract.ViewModels.UI.Reports
{


    public class ContractReportModel : INotifyPropertyChanged
    {
        [DisplayName("Наименование заказчика")]
        public  string Client { get; set; }

        [DisplayName("Наименование объекта")]
        public  string Object { get; set; }

        [DisplayName("Номер договора")]
        public  string Number { get; set; }

        [DisplayName("Дата договора")]
        public  DateTime? Date { get; set; }

        [DisplayName("Сумма договора")]
        public virtual double? Amount { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));

        }

        public int Id { get; set; }
    }

}
