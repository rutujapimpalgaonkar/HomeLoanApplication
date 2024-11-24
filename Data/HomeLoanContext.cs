using Microsoft.EntityFrameworkCore;
using HomeLoanApplication.Models;

namespace HomeLoanApplication.Data
{
    public class HomeLoanContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<IncomeDetail> IncomeDetails { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<LoanTracker> LoanTrackers { get; set; }

        public HomeLoanContext(DbContextOptions<HomeLoanContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Configure primary key for LoanTracker
    modelBuilder.Entity<LoanTracker>()
        .HasKey(t => t.TrackerId);

    // Explicitly configure primary key for LoanApplication
    modelBuilder.Entity<LoanApplication>()
        .HasKey(l => l.ApplicationId);

    // Configure one-to-many relationship between LoanApplication and Document
    modelBuilder.Entity<Document>()
        .HasOne(d => d.LoanApplication)
        .WithMany(l => l.Documents)
        .HasForeignKey(d => d.ApplicationId)
        .OnDelete(DeleteBehavior.SetNull);  // Use SET NULL instead of RESTRICT

    // Make sure ApplicationId is nullable (already done in the model)
    modelBuilder.Entity<Document>()
        .Property(d => d.ApplicationId)
        .IsRequired(false);  // Explicitly make ApplicationId nullable (this step should not be necessary if the model already has int? type)

    // Configure one-to-many relationship between LoanApplication and IncomeDetail
    modelBuilder.Entity<IncomeDetail>()
        .HasOne(i => i.LoanApplication)
        .WithMany(l => l.IncomeDetails)
        .HasForeignKey(i => i.ApplicationId)
        .OnDelete(DeleteBehavior.Restrict);

    // Configure one-to-one relationship between Account and LoanApplication
    modelBuilder.Entity<Account>()
        .HasOne(a => a.LoanApplication)
        .WithMany()
        .HasForeignKey(a => a.ApplicationId)
        .OnDelete(DeleteBehavior.Restrict);

    // Configure one-to-one relationship between LoanTracker and LoanApplication
    modelBuilder.Entity<LoanTracker>()
        .HasOne(t => t.LoanApplication)
        .WithOne(l => l.LoanTracker)
        .HasForeignKey<LoanTracker>(t => t.ApplicationId)
        .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete
}

    }
}
