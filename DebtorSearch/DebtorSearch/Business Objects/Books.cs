using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DebtorSearch.Business_Objects
{
    [Table("Books")]
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string UserName { get; set; }
        public string BookName { get; set; }
      
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? Timestamp { get; set; }

    }
}