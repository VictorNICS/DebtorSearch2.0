using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DebtorSearch.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmployeeNo { get; set; }
        public string Department { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
    
    }
}