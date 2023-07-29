using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebAppApi.RefreshToken
{
    public partial class SaleOnlineContext : DbContext
    {
        public SaleOnlineContext()
        {
        }

        public SaleOnlineContext(DbContextOptions<SaleOnlineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshToken");

                entity.HasIndex(e => e.UserId, "IX_RefreshToken_UserID");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
