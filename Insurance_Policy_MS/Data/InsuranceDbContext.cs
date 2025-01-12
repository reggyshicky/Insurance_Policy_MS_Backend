using Insurance_Policy_MS.Models;
using Microsoft.EntityFrameworkCore;

namespace Insurance_Policy_MS.Data
{
    public class InsuranceDbContext : DbContext
    {
        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options)
            : base(options)
        {
        }

        public DbSet<InsurancePolicy> Policies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Policy entity
            modelBuilder.Entity<InsurancePolicy>(entity =>
            {
                // Use Table attribute instead of ToTable method
                entity.ToTable(name: "policies");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.PolicyNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PolicyHolderName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PolicyHolderAddress)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PolicyHolderEmail)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PolicyHolderPhone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CoverageAmount)
                    .HasPrecision(18, 2);  

                entity.Property(e => e.Premium)
                    .HasPrecision(18, 2); 

                entity.HasIndex(e => e.PolicyNumber)
                    .IsUnique();

                entity.HasIndex(e => e.PolicyHolderEmail);
            });
        }
    }
}