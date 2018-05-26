using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contract.Data.Models.Base;

namespace Contract.Data.Models
{
    [Table("Organizations")]
    public class Organization : BaseEntity
    {
        public string Name { get; set; }
        
        [MaxLength(9)]
        [Index(IsUnique = true)]
        public string Inn { get; set; }

        public string AccountNumber { get; set; } 

        public string PhoneNumbers { get; set; }
         
        public string TypeOwnership { get; set; }

        public string Chief { get; set; }

        public string Position { get; set; }

        public string Foundation { get; set; }

        public string LegalAddress { get; set; }

        public string Postcode { get; set; }

        public string BankName1 { get; set; }

        public string Mfo1 { get; set; }

        public string LS { get; set; }
         
        public string BankName2 { get; set; }

        public string Mfo2 { get; set; }

        public string KS { get; set; }

        public string BankName3 { get; set; }

        public string Mfo3 { get; set; }

        public string Okohx { get; set; }
         
        public ICollection<Contract> Contracts { get; set; }

        public Organization()
        {
            Contracts = new List<Contract>();
        }

    }
}