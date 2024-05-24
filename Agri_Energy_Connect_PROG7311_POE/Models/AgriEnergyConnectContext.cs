using AgriEnergyConnectApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

#nullable disable

namespace AgriEnergyConnectApp.Models
{
    // Database context class for AgriEnergyConnect
    public partial class AgriEnergyConnectContext : DbContext
    {
        // Default constructor for the context
        public AgriEnergyConnectContext()
        {
        }

        // Constructor with options for the context
        public AgriEnergyConnectContext(DbContextOptions<AgriEnergyConnectContext> options)
            : base(options)
        {
        }
        // DbSet for the Employee entity
        // DbSet for the Farmer entity
        // DbSet for the Product entity

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Farmer> Farmers { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        // Method to configure database options if not already configured

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:agri-energy-connect-server.database.windows.net,1433;Initial Catalog=AgriEnergyConnect;Persist Security Info=False;User ID=aariya;Password=B1ngus@rc;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Employee_ID)
                    .HasName("PK__Employee__59AF14B517A630CE");

                entity.ToTable("Employee");

                entity.Property(e => e.Employee_ID)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Farmer>(entity =>
            {
                entity.HasKey(e => e.Farmer_ID)
                    .HasName("PK_Farmer");

                entity.ToTable("Farmer");

                entity.Property(e => e.Farmer_ID)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FarmerName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Product_ID)
                   .HasName("PK_Poduct");
                entity.ToTable("Product");

                entity.Property(e => e.Product_ID).HasColumnName("Product_ID");

                entity.Property(e => e.Farmer_ID)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Product_Date).HasColumnType("date");

                entity.Property(e => e.Product_Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Product_Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Product_Quantity).HasColumnName("Product_Quantity");

                entity.Property(e => e.Product_Type)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Farmer)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Farmer_ID)
                    .HasConstraintName("FK_Product_Farmer");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
