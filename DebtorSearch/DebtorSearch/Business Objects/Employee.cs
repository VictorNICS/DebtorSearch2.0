namespace DebtorSearch.Business_Objects
{
   
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(10)]
        public string EmployeeNumber { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string Surname { get; set; }

        public bool? Deleted { get; set; }

        public int RoleID { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Altered { get; set; }

        public virtual EmployeeRole EmployeeRole { get; set; }
    }
}
