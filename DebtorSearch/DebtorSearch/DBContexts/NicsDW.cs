namespace DebtorSearch.Business_Objects
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using DebtorSearch.Business_Objects;
    
    public partial class NicsDW : DbContext
    {
        public NicsDW()
            : base("name=NICSDW")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeRole>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeRole>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.EmployeeRole)
                .HasForeignKey(e => e.RoleID)
                .WillCascadeOnDelete(false);
        }
    }
}
