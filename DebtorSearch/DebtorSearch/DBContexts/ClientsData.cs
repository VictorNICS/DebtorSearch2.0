namespace DebtorSearch.Business_Objects
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ClientsData : DbContext
    {
        public ClientsData()
            : base("name=ClientsData")
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Debtor> Debtors { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .Property(e => e.AccountNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.Address1)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.Address2)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.Address3)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.Address4)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.Address5)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.AddressCode)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.ClientName)
                .IsUnicode(false);

            modelBuilder.Entity<Debtor>()
                .Property(e => e.AccountNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Debtor>()
                .Property(e => e.ClientRef)
                .IsUnicode(false);

            modelBuilder.Entity<Debtor>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Debtor>()
                .Property(e => e.Initials)
                .IsUnicode(false);

            modelBuilder.Entity<Debtor>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Debtor>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Debtor>()
                .Property(e => e.IdNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Debtor>()
                .Property(e => e.ContactNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Debtor>()
                .Property(e => e.ClientName)
                .IsUnicode(false);

            modelBuilder.Entity<Debtor>()
                .Property(e => e.Vat)
                .IsUnicode(false);

            modelBuilder.Entity<Debtor>()
                .Property(e => e.PassportNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Email>()
                .Property(e => e.AccountNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Email>()
                .Property(e => e.Emails)
                .IsUnicode(false);

            modelBuilder.Entity<Email>()
                .Property(e => e.ClientName)
                .IsUnicode(false);
        }
    }
}
