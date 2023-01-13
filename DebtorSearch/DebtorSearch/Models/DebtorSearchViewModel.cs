using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DebtorSearch.Models
{
    public class DebtorSearchViewModel
    {
        [Display(Name = "ID No.")]
        public string IdNumber { get; set; }
        [Display(Name = "Surname / Company")]
        public string surname { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Contact No.")]
        public string ContactNo { get; set; }
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }
        [Display(Name = "Account No.")]
        public string AccountNumber { get; set; }
        [Display(Name = "Client Ref No.")]
        public string ClientRef { get; set; }
        [Display(Name = "Email Address")]
        public string EmailAddres { get; set; }
        [Display(Name = "Address Line ")]
        public string Address1 { get; set; }
        [Display(Name = "Address Line")]
        public string Address2 { get; set; }
        [Display(Name = "Postal Code")]
        public string Code { get; set; }
        [Display(Name = "Vat No.")]
        public string VatNo { get; set; }
        [Display(Name = "Company Registration No.")]
        public string CompanyReg { get; set; }
        public List<DebtorSearchViewModel> DebtorSearchViewModelList { get; set; }

        public bool Empty
        {
            get
            {
                return (

                    string.IsNullOrWhiteSpace(IdNumber) &&
                    string.IsNullOrWhiteSpace(surname) &&
                    string.IsNullOrWhiteSpace(Name) &&
                    string.IsNullOrWhiteSpace(EmailAddres) &&
                    string.IsNullOrWhiteSpace(ContactNo) &&
                    string.IsNullOrWhiteSpace(AccountNumber) &&
                    string.IsNullOrWhiteSpace(ClientRef) &&
                    string.IsNullOrWhiteSpace(Address1) &&
                    string.IsNullOrWhiteSpace(Address2) &&
                     string.IsNullOrWhiteSpace(VatNo) &&
                      string.IsNullOrWhiteSpace(CompanyReg) 
                   );
            }
        }
    }
}