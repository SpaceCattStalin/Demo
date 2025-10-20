using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Entities;

namespace API.Seeders
{
    public class Seeder(DemoDbContext dbContext)
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Roles.Any())
                {
                    var roles = GetPrimaryRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
                if (!dbContext.Users.Any())
                {
                    var users = GetPrimaryUsers();
                    dbContext.Users.AddRange(users);
                    await dbContext.SaveChangesAsync();
                }
                if (!dbContext.Products.Any())
                {
                    var products = GetPrimaryProducts();
                    dbContext.Products.AddRange(products);
                    await dbContext.SaveChangesAsync();
                }

            }
        }
        private IEnumerable<Role> GetPrimaryRoles()
        {
            return new List<Role>
            {
                new() { Name = "Admin" },
                new() { Name = "User" },
            };
        }

        private IEnumerable<User> GetPrimaryUsers()
        {
            return new List<User>
            {
                new User
                {
                    RoleId = 1,
                    Name = "admin",
                    Email = "admin@example.com",
                    Password = "123",
                    Address = "123 Admin Street",
                    Phone = "0901234567",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                },
                new User
                {
                    RoleId = 2,
                    Name = "user",
                    Email = "user@example.com",
                    Password = "123",
                    Address = "456 User Avenue",
                    Phone = "0987654321",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                }
            };
        }
        private IEnumerable<Product> GetPrimaryProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Name = "Chuột Gaming",
                    Description = "Chuột không dây RGB",
                    Price = 350000.00m,
                    StockQuantity = 50,
                    ImageUrl = "http://localhost:5140/Images/chuot_gaming.jpg",
                    CreatedDate = DateTime.UtcNow,
                    IsAvailable = true,
                },
                new Product
                {
                    Name = "Bàn phím Cơ",
                    Description = "Bàn phím cơ Blue Switch",
                    Price = 950000.00m,
                    StockQuantity = 30,
                    ImageUrl = "http://localhost:5140/Images/ban_him_co.png",
                    CreatedDate = DateTime.UtcNow,
                    IsAvailable = true,
                },
                new Product
                {
                    Name = "Tai nghe Bluetooth",
                    Description = "Tai nghe chống ồn",
                    Price = 500000.00m,
                    StockQuantity = 75,
                    ImageUrl = "http://localhost:5140/Images/tai_nghe.jpg",
                    CreatedDate = DateTime.UtcNow,
                    IsAvailable = true,
                },
                new Product
                {
                    Name = "Màn hình 24 inch",
                    Description = "Màn hình Full HD 144Hz",
                    Price = 3200000.00m,
                    StockQuantity = 20,
                    ImageUrl = "http://localhost:5140/Images/man_hinh.jpg",
                    CreatedDate = DateTime.UtcNow,
                    IsAvailable = true,
                },
                new Product
                {
                    Name = "Laptop Văn phòng",
                    Description = "Laptop i5 8GB RAM",
                    Price = 15000000.00m,
                    StockQuantity = 15,
                    ImageUrl = "http://localhost:5140/Images/laptop.jpg",
                    CreatedDate = DateTime.UtcNow,
                    IsAvailable = true,
                },
                new Product
                {
                    Name = "Webcam HD",
                    Description = "Webcam call 1080p",
                    Price = 420000.00m,
                    StockQuantity = 60,
                    ImageUrl = "http://localhost:5140/Images/webcam.jpg",
                    CreatedDate = DateTime.UtcNow,
                    IsAvailable = true,
                },
                new Product
                {
                    Name = "Loa Bluetooth",
                    Description = "Loa mini di động",
                    Price = 380000.00m,
                    StockQuantity = 85,
                    ImageUrl = "http://localhost:5140/Images/loa.jpg",
                    CreatedDate = DateTime.UtcNow,
                    IsAvailable = true,
                },
                new Product
                {
                    Name = "USB 64GB",
                    Description = "USB 3.0 tốc độ cao",
                    Price = 180000.00m,
                    StockQuantity = 100,
                    ImageUrl = "http://localhost:5140/Images/usb.jpg",
                    CreatedDate = DateTime.UtcNow,
                    IsAvailable = true,
                }
            };
        }
    }
}
