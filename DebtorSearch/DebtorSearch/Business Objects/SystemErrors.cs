using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DebtorSearch.Business_Objects
{
    [Table("SystemErrors")]
    public class SystemErrors
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ErrorId { get; set; }

        public string SessionId { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorStackTrace { get; set; }
        public string ErrorSource { get; set; }
        public string SystemUser { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? ErrorDate { get; set; }
    }
}