namespace DebtorSearch.Business_Objects
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SearchHistory")]
    public partial class SearchHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Column(TypeName = "xml")]
        [Required]
        public string SearchLog { get; set; }

        [Required]
        [StringLength(30)]
        public string MachineName { get; set; }

        [Required]
        [StringLength(50)]
        public string WindowsUserName { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime TimeStamp { get; set; }

        public int SystemType { get; set; }
    }
}
