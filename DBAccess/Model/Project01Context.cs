using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DBAccess.Model
{
    public partial class Project01Context : DbContext
    {
        public Project01Context()
        {
        }

        public Project01Context(DbContextOptions<Project01Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<OrderHistory> OrderHistory { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => new { e.LocationId, e.ProductId });

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.LocationId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<OrderHistory>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Time)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.OrderHistory)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.OrderHistory)
                    .HasForeignKey(d => d.LocationId);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PK_Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Order_OrderHistory_OrderID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Order_Product_ProductID");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnType("numeric(10, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
