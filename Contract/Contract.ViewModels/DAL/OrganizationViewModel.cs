using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Contract.ViewModels.DAL
{
    public class OrganizationViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Inn { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string PhoneNumbers { get; set; }

        [Required]
        public string TypeOwnership { get; set; }

        [Required]
        public string Chief { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Foundation { get; set; }

        [Required]
        public string LegalAddress { get; set; }

        [Required]
        public string Postcode { get; set; }

        public string BankName1 { get; set; }

        public string Mfo1 { get; set; }

        public string LS { get; set; }

        public string BankName2 { get; set; }

        public string Mfo2 { get; set; }

        public string KS { get; set; }

        public string BankName3 { get; set; }

        public string Mfo3 { get; set; }

        [Required]
        public string Okohx { get; set; }

    }
}