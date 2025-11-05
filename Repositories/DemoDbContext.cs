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
        public virtual DbSet<CartItem> CartItems { get; set; }

        public virtual DbSet<DiscountCode> DiscountCodes { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

        public virtual DbSet<CategoryImage> CategoryImages { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductVariant> ProductVariants { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ImageType> ImageTypes { get; set; }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Shipping> Shippings { get; set; }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<ProductSize> ProductSizes { get; set; }

        public virtual DbSet<UsersDiscountCode> UsersDiscountCodes { get; set; }
        public virtual DbSet<PendingEmailRegistration> PendingEmailRegistrations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ---------------- CART ----------------
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Cart__3214EC074C214C3E");

                entity.ToTable("Cart");

                //entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                //entity.HasOne(d => d.Orders).WithMany(p => p.Carts)
                //    .HasForeignKey(d => d.OrdersId)
                //    .HasConstraintName("FKCart841263");

                entity.HasMany(c => c.Items)
                    .WithOne(i => i.Cart)
                    .HasForeignKey(i => i.CartId)
                    .HasConstraintName("FK__Cart_CartItem");

                entity.HasOne(d => d.Users).WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCart661383");
            });
            // ---------------- CART ITEM ----------------
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__CartItem");

                entity.ToTable("CartItem");

                entity.HasOne(ci => ci.Cart)
                      .WithMany(c => c.Items)
                      .HasForeignKey(ci => ci.CartId)
                      .HasConstraintName("FK_CartItem_Cart");

                entity.HasOne(ci => ci.ProductVariant)
                      .WithMany()
                      .HasForeignKey(ci => ci.ProductVariantId)
                      .HasConstraintName("FK_CartItem_ProductVariant");

                entity.HasOne(ci => ci.Size)
                      .WithMany()
                      .HasForeignKey(ci => ci.SizeId)
                      .HasConstraintName("FK_CartItem_Size");
            });

            // ---------------- DISCOUNT CODE ----------------
            modelBuilder.Entity<DiscountCode>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Discount__3214EC076EA21809");

                entity.ToTable("DiscountCode");

                entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
                entity.Property(e => e.MinimumAmount).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Value).HasColumnType("decimal(10, 2)");
            });

            // ---------------- ORDER ----------------
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC0725F77EBC");

                entity.Property(e => e.CreatedDate).HasColumnType("int");
                entity.Property(e => e.Status).HasMaxLength(100);
                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.UpdatedDate).HasColumnType("int");

                entity.HasMany(o => o.Items)
                    .WithOne(i => i.Order)
                    .HasForeignKey(i => i.OrderId)
                    .HasConstraintName("FK__OrderItem_Order");


                entity.HasOne(d => d.Users).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOrders336570");

                entity.HasMany(o => o.Payments)
                    .WithOne(p => p.Order)
                    .HasForeignKey(p => p.OrderId)
                    .HasConstraintName("FK_Payments_Order");
            });
            // ---------------- ORDER ITEM----------------
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__OrderItem");

                entity.ToTable("OrderItem");

                entity.HasOne(oi => oi.Order)
                      .WithMany(i => i.Items)
                      .HasForeignKey(oi => oi.OrderId)
                      .HasConstraintName("FK__OrderItem_Order");

                entity.HasOne(oi => oi.ProductVariant)
                      .WithMany()
                      .HasForeignKey(oi => oi.ProductVariantId)
                      .HasConstraintName("FK__OrderItem_ProductVariant");

                entity.HasOne(oi => oi.Size)
                      .WithMany()
                      .HasForeignKey(oi => oi.SizeId)
                      .HasConstraintName("FK__OrderItem_Size");

            });
            // ---------------- PAYMENT METHOD ----------------
            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__PaymentMethod");
                entity.ToTable("PaymentMethod");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            // ---------------- PAYMENT ----------------
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC07B7C873C2");

                entity.ToTable("Payment");

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.CreatedDate).HasColumnType("int");
                entity.Property(e => e.ProcessedDate).HasColumnType("int");
                entity.Property(e => e.Status).HasMaxLength(20);
                entity.Property(e => e.UpdatedDate).HasColumnType("int");

                entity.HasOne(p => p.Method)
                      .WithMany(m => m.Payments)
                      .HasForeignKey(p => p.PaymentMethodId)
                      .HasConstraintName("FK_Payment_PaymentMethod");

                entity.HasOne(p => p.Order)
                    .WithMany(o => o.Payments)
                    .HasForeignKey(p => p.OrderId)
                    .HasConstraintName("FK_Order_Payments");
            });

            // ---------------- PRODUCT ----------------
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Product__3214EC072751BF49");

                entity.ToTable("Product");

                //entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.Description).HasMaxLength(55).IsRequired(false);
                entity.Property(e => e.Name).HasMaxLength(55);
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
                //entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
                entity.Property(e => e.CreatedAt).HasColumnType("int");
                entity.Property(e => e.UpdatedAt).HasColumnType("int");
            });

            // ---------------- SIZE ----------------
            modelBuilder.Entity<Size>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Size");
                entity.ToTable("Size");

                entity.Property(e => e.SizeType).HasMaxLength(20);
            });

            // ---------------- PRODUCT SIZE ----------------
            modelBuilder.Entity<ProductSize>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ProductSize");

                entity.ToTable("ProductSize");


                entity.HasOne(ps => ps.Size)
                    .WithMany(s => s.ProductSize)
                    .HasForeignKey(ps => ps.SizeId)
                    .HasConstraintName("FK_ProductSize_Size");

                //entity.HasOne(ps => ps.Product)
                //    .WithMany(p => p.ProductSize)
                //    .HasForeignKey(ps => ps.ProductId)
                //    .HasConstraintName("FK_ProductSize_Product");

                entity.HasOne(ps => ps.ProductVariant)
                    .WithMany(pv => pv.Sizes)
                    .HasForeignKey(ps => ps.ProductVariantId)
                    .HasConstraintName("FK_ProductSize_ProductVariant");
            });

            // ---------------- PRODUCT VARIANT ----------------
            modelBuilder.Entity<ProductVariant>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ProductVariant");

                entity.ToTable("ProductVariant");

                entity.Property(e => e.VariantCode).HasMaxLength(50);
                entity.Property(e => e.Color).HasMaxLength(10);
                entity.Property(e => e.CreatedAt).HasColumnType("int");
                entity.Property(e => e.UpdatedAt).HasColumnType("int");
                entity.Property(e => e.StockQuantity).HasColumnType("int");


                entity.HasMany(v => v.Images)
                      .WithOne(i => i.ProductVariant)
                      .HasForeignKey(i => i.ProductVariantId)
                      .HasConstraintName("FK_ProductImage_ProductVariant");
            });

            // ---------------- PRODUCT IMAGE ----------------
            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_ProductImage");

                entity.ToTable("ProductImage");

                entity.Property(e => e.Url)
                      .IsRequired()
                      .HasMaxLength(200);
                entity.Property(e => e.CreatedAt).HasColumnType("int");
                entity.Property(e => e.UpdatedAt).HasColumnType("int");

                entity.HasOne(i => i.Product)
                      .WithMany(p => p.Images)
                      .HasForeignKey(i => i.MainProductId)
                      .HasConstraintName("FK_ProductImage_Product");

                entity.HasOne(i => i.ProductVariant)
                      .WithMany(v => v.Images)
                      .HasForeignKey(i => i.ProductVariantId)
                      .HasConstraintName("FK_ProductImage_ProductVariant");

                entity.HasOne(i => i.ImageType)
                      .WithMany()
                      .HasForeignKey(i => i.ImageTypeId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_ProductImage_ImageType");
            });

            // ---------------- IMAGE TYPE ----------------
            modelBuilder.Entity<ImageType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_ImageType");

                entity.ToTable("ImageType");

                entity.Property(e => e.Code)
                      .HasMaxLength(20)
                      .IsUnicode(false)
                      .IsRequired();

                entity.Property(e => e.Name)
                      .HasMaxLength(100)
                      .IsRequired();
            });

            // ---------------- REFRESH TOKEN ----------------
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

            // ---------------- ROLE ----------------
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId).HasName("PK__Role__3214EC078D93600C");

                entity.ToTable("Role");

                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            // ---------------- SHIPPING ----------------
            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Shipping__3214EC07193B379B");

                entity.ToTable("Shipping");
                entity.Property(e => e.FinishDate).HasColumnType("int");
                entity.Property(e => e.StartDate).HasColumnType("int");
                entity.Property(e => e.Status).HasMaxLength(20);

                entity.HasOne(d => d.Order).WithMany(p => p.Shippings)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKShipping479047");
            });

            // ---------------- USER ----------------
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07253DC382");

                entity.Property(e => e.Address).HasMaxLength(50);
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.Password).HasMaxLength(255).IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Role).WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUsers969682");


            });

            // ---------------- USER DISCOUNT CODE ----------------
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

            // ---------------- CATEGORY ----------------
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId).HasName("PK__Category__3214EC07XXXXXX");
                entity.Property(e => e.CategoryId).ValueGeneratedNever();

                entity.ToTable("Category");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsRequired();

                entity.HasMany(c => c.Products)
                    .WithOne(p => p.Category)
                    .HasForeignKey(c => c.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Product_Category");

                entity.HasOne(c => c.Image)
                    .WithOne(i => i.Category)
                    .HasForeignKey<CategoryImage>(i => i.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CategoryImage_Category");
            });

            // ---------------- CATEGORY ----------------
            modelBuilder.Entity<CategoryImage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__CategoryImage__3214EC07");

                entity.ToTable("CategoryImage");

                entity.Property(e => e.Url)
                    .HasMaxLength(200);
            });

            // ---------------- PENDING EMAIL REGISTRATION ----------------
            modelBuilder.Entity<PendingEmailRegistration>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__PendingEmailRegistration");
                entity.ToTable("PendingEmailRegistration");

                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.PasswordHash).HasMaxLength(255).IsUnicode(false);

                entity.Property(e => e.OtpHash).HasMaxLength(128).IsUnicode(false);
                entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");
                entity.Property(e => e.ExpiresAtUtc).HasColumnType("datetime");
                entity.Property(e => e.LastSentAtUtc).HasColumnType("datetime");

                entity.HasIndex(e => e.Email);
            });



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
