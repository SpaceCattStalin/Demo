using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories.Entities;
using System.Data;

namespace Repositories
{
    public partial class DemoDbContext : DbContext
    {
        public DemoDbContext()
        {
        }

        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }

        public virtual DbSet<DiscountCode> DiscountCodes { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Payment> Payments { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Shipping> Shippings { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UsersDiscountCode> UsersDiscountCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Cart__3214EC074C214C3E");

                entity.ToTable("Cart");

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Orders).WithMany(p => p.Carts)
                    .HasForeignKey(d => d.OrdersId)
                    .HasConstraintName("FKCart841263");

                entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCart557549");

                entity.HasOne(d => d.Users).WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCart661383");
            });

            modelBuilder.Entity<DiscountCode>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Discount__3214EC076EA21809");

                entity.ToTable("DiscountCode");

                entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
                entity.Property(e => e.MinimumAmount).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Value).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC0725F77EBC");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.Status).HasMaxLength(100);
                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Users).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOrders336570");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC07B7C873C2");

                entity.ToTable("Payment");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.ProcessedDate).HasColumnType("datetime");
                entity.Property(e => e.Status).HasMaxLength(100);
                entity.Property(e => e.Type).HasMaxLength(100);
                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdNavigation).WithOne(p => p.Payment)
                    .HasForeignKey<Payment>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPayment927541");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Product__3214EC072751BF49");

                entity.ToTable("Product");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.ImageUrl).IsUnicode(false);
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__RefreshT__3214EC0720F03FBF");

                entity.ToTable("RefreshToken");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Users).WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKRefreshTok382511");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Role__3214EC078D93600C");

                entity.ToTable("Role");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Shipping__3214EC07193B379B");

                entity.ToTable("Shipping");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.FinishDate).HasColumnType("datetime");
                entity.Property(e => e.StartDate).HasColumnType("datetime");
                entity.Property(e => e.Status).HasMaxLength(100);

                entity.HasOne(d => d.IdNavigation).WithOne(p => p.Shipping)
                    .HasForeignKey<Shipping>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKShipping479047");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07253DC382");

                entity.Property(e => e.Address).HasMaxLength(255);
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Role).WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUsers969682");

                
            });

            modelBuilder.Entity<UsersDiscountCode>(entity =>
            {
                entity.HasKey(e => new { e.UsersId, e.DiscountCodeId }).HasName("PK__Users_Di__15BAA66E5D867B73");

                entity.ToTable("Users_DiscountCode");

                entity.HasOne(d => d.DiscountCode).WithMany(p => p.UsersDiscountCodes)
                    .HasForeignKey(d => d.DiscountCodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUsers_Disc368718");

                entity.HasOne(d => d.Users).WithMany(p => p.UsersDiscountCodes)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUsers_Disc661265");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
