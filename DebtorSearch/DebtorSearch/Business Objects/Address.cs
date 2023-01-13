namespace DebtorSearch.Business_Objects
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Address")]
    public partial class Address
    {
        public int AddressID { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        [StringLength(50)]
        public string Address1 { get; set; }

        [StringLength(50)]
        public string Address2 { get; set; }

        [StringLength(50)]
        public string Address3 { get; set; }

        [StringLength(50)]
        public string Address4 { get; set; }

        [StringLength(50)]
        public string Address5 { get; set; }

        [StringLength(50)]
        public string AddressCode { get; set; }

        [StringLength(50)]
        public string ClientName { get; set; }
    }
}
