using Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ContexDb
{
   public class SaleContexDb : DbContext
    {
        public SaleContexDb(DbContextOptions options):base(options){ }

        #region DBSET
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("Product");
                e.HasIndex(e => e.ProductName).IsUnique();
                
            });
            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("Category");
                e.Property(e => e.CategoryID).ValueGeneratedOnAdd();

            });
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("User");

            });
            modelBuilder.Entity<Order>(e =>
            {
                e.ToTable("Order");
                e.Property(pe => pe.OrderDate).HasDefaultValueSql("getutcdate()");
            });
            modelBuilder.Entity<OrderDetails>(e =>
            {
                e.ToTable("OrderDetails");
                e.HasKey(e => new { e.OrderId, e.ProductID });
                e.HasOne(e => e.product)
                 .WithMany(e => e.orderDetails)
                 .HasForeignKey(e => e.ProductID)
                 .HasConstraintName("FK_Orderdetail_Product");

         
                e.HasOne(e => e.order)
                 .WithMany(e => e.orderDetails)
                 .HasForeignKey(e => e.OrderId)
                 .HasConstraintName("FK_Orderdetail_Order");

            });
        }
        #endregion


    }
}
