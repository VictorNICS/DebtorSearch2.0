namespace DebtorSearch.Business_Objects
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NicsDebtorSearch : DbContext
    {
        public NicsDebtorSearch()
            : base("name=NicsDebtorSearch")
        {
        }


        public virtual DbSet<SearchHistory> SearchHistories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<SystemErrors> SystemError { get; set; }
        public virtual DbSet<Books> Books { get; set; }
  public virtual DbSet<ConnectionStrings> ConnectionStrings { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Role>()
        //        .HasMany(e => e.Users)
        //        .WithMany(e => e.Roles)
        //        .Map(m => m.ToTable("UserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));
        //}
    }
}
