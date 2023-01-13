using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DebtorSearch.Business_Objects
{
    [Table("ConnectionStrings")]
    public class ConnectionStrings
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(50)]
        public string ServerName { get; set; }
        [StringLength(50)]
        public string UserId { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(100)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string ConString { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Timestamp { get; set; }
    }
}