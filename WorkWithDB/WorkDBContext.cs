namespace WorkWithDB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WorkDBContext : DbContext
    {
        public WorkDBContext()
            : base("name=WorkDBContext")
        {
        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<ContractStat> ContractStat { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(e => e.CompanyName)
                .IsFixedLength();

            modelBuilder.Entity<Company>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContractStat>()
                .Property(e => e.StatName)
                .IsFixedLength();

            modelBuilder.Entity<ContractStat>()
                .HasMany(e => e.Company)
                .WithRequired(e => e.ContractStat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Login)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsFixedLength();
        }
    }
}
