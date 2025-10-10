using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories.Entities;

namespace Repositories
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext() : base()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        //public static string GetConnectionString(string connectionStringName)
        //{
        //    var config = new ConfigurationBuilder()
        //        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //        .AddJsonFile("appsettings.json")
        //        .Build();

        //    string connectionString = config.GetConnectionString(connectionStringName);
        //    return connectionString;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasData(
           new User
           {
               UserId = 1,
               Username = "nguyenvana",
               PasswordHash = "hash123",
               Email = "an@example.com"
           },
           new User
           {
               UserId = 2,
               Username = "lethib",
               PasswordHash = "hash456",
               Email = "b@example.com"
           }
       );

            // 🛒 CARTS (mỗi user 1 cart)
            modelBuilder.Entity<Cart>().HasData(
                new Cart { CartId = 1, UserId = 1 },
                new Cart { CartId = 2, UserId = 2 }
            );

            // 🛍 PRODUCTS (8 sản phẩm tiếng Việt)
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Chuột Gaming", Description = "Chuột không dây RGB", Price = 350000, ImageUrl = "http://localhost:5140/Images/chuot_gaming.jpg" },
                new Product { ProductId = 2, Name = "Bàn phím Cơ", Description = "Bàn phím cơ Blue Switch", Price = 950000, ImageUrl = "http://localhost:5140/Images/ban_him_co.png" },
                new Product { ProductId = 3, Name = "Tai nghe Bluetooth", Description = "Tai nghe chống ồn", Price = 500000, ImageUrl = "http://localhost:5140/Images/tai_nghe.jpg" },
                new Product { ProductId = 4, Name = "Màn hình 24 inch", Description = "Màn hình Full HD 144Hz", Price = 3200000, ImageUrl = "http://localhost:5140/Images/man_hinh.jpg" },
                new Product { ProductId = 5, Name = "Laptop Văn phòng", Description = "Laptop i5 8GB RAM", Price = 15000000, ImageUrl = "http://localhost:5140/Images/laptop.jpg" },
                new Product { ProductId = 6, Name = "Webcam HD", Description = "Webcam call 1080p", Price = 420000, ImageUrl = "http://localhost:5140/Images/webcam.jpg" },
                new Product { ProductId = 7, Name = "Loa Bluetooth", Description = "Loa mini di động", Price = 380000, ImageUrl = "http://localhost:5140/Images/loa.jpg" },
                new Product { ProductId = 8, Name = "USB 64GB", Description = "USB 3.0 tốc độ cao", Price = 180000, ImageUrl = "http://localhost:5140/Images/usb.jpg" }
            );

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }

}
