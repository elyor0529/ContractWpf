using Contract.Data.Models.Enums;

namespace Contract.ViewModels.DAL
{
    public class WorkTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descrption { get; set; }
        public WorkTypeCode Code { get; set; } 
        public override string ToString()
        {
            return Descrption;
        }
    }
}