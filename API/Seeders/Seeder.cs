//using Microsoft.EntityFrameworkCore;
//using Repositories;
//using Repositories.Entities;
//using Services.Utils;

//namespace API.Seeders
//{
//    public class Seeder(DemoDbContext dbContext)
//    {
//        public async Task Seed()
//        {
//            if (await dbContext.Database.CanConnectAsync())
//            {
//                if (!dbContext.Roles.Any())
//                {
//                    var roles = GetPrimaryRoles();
//                    dbContext.Roles.AddRange(roles);
//                    await dbContext.SaveChangesAsync();
//                }
//                if (!dbContext.Users.Any())
//                {
//                    var users = GetPrimaryUsers();
//                    dbContext.Users.AddRange(users);
//                    await dbContext.SaveChangesAsync();
//                }
//                if (!dbContext.Sizes.Any())
//                {
//                    var sizes = GetSizes();
//                    dbContext.Sizes.AddRange(sizes);
//                    await dbContext.SaveChangesAsync();
//                }

//                if (!dbContext.Categories.Any())
//                {
//                    var categories = GetCategories();
//                    dbContext.Categories.AddRange(categories);
//                    await dbContext.SaveChangesAsync();
//                }

//                if (!dbContext.CategoryImages.Any())
//                {
//                    var categoryImages = GetCategoryImages();
//                    dbContext.CategoryImages.AddRange(categoryImages);
//                    await dbContext.SaveChangesAsync();
//                }

//                if (!dbContext.ImageTypes.Any())
//                {
//                    var images = GetImageType();
//                    dbContext.ImageTypes.AddRange(images);
//                    await dbContext.SaveChangesAsync();
//                }

//                if (!dbContext.PaymentMethods.Any())
//                {
//                    var paymentMethods = GetPaymentMethods();
//                    dbContext.PaymentMethods.AddRange(paymentMethods);
//                    await dbContext.SaveChangesAsync();
//                }

//                if (!dbContext.Products.Any())
//                {
//                    var products = GetPrimaryProducts();
//                    dbContext.Products.AddRange(products);
//                    await dbContext.SaveChangesAsync();
//                }

//                if (!dbContext.ProductSizes.Any())
//                {
//                    var productSizes = GetProductSizes();
//                    dbContext.ProductSizes.AddRange(productSizes);
//                    await dbContext.SaveChangesAsync();
//                }
//            }
//        }
//        private IEnumerable<Role> GetPrimaryRoles()
//        {
//            return new List<Role>
//            {
//                new() { RoleId = 1,Name = "Admin" },
//                new() { RoleId = 2, Name = "User" },
//            };
//        }

//        private IEnumerable<User> GetPrimaryUsers()
//        {
//            return new List<User>
//            {
//                new User
//                {
//                    RoleId = 1,
//                    Name = "admin",
//                    Email = "admin@example.com",
//                    Password = "123",
//                    Address = "123 Admin Street",
//                    Phone = "0901234567",
//                    CreatedDate = DateTime.UtcNow,
//                    UpdatedDate = DateTime.UtcNow
//                },
//                new User
//                {
//                    RoleId = 2,
//                    Name = "user",
//                    Email = "user@example.com",
//                    Password = "123",
//                    Address = "456 User Avenue",
//                    Phone = "0987654321",
//                    CreatedDate = DateTime.UtcNow,
//                    UpdatedDate = DateTime.UtcNow
//                }
//            };
//        }

//        private IEnumerable<CategoryImage> GetCategoryImages()
//        {
//            return new List<CategoryImage>
//            {
//                new CategoryImage
//                {
//                    CategoryId = 1,
//                    Url = "/Images/Categories/vi_photo_shoot_1.jpg"
//                },
//                new CategoryImage
//                {
//                    CategoryId = 2,
//                    Url = "/Images/Categories/that_lung_photo_shoot_1.jpg"
//                },
//                new CategoryImage
//                {
//                    CategoryId = 3,
//                    Url = "/Images/Categories/tui_photo_shoot_1.jpg"
//                },
//                new CategoryImage
//                {
//                    CategoryId = 4,
//                    Url = "/Images/Categories/tat_photo_shoot_1.jpg"
//                },
//                new CategoryImage
//                {
//                    CategoryId = 5,
//                    Url = "/Images/Categories/ca_vat_photo_shoot_1.jpg"
//                },
//                new CategoryImage
//                {
//                    CategoryId = 6,
//                    Url = "/Images/Categories/giay_photo_shoot_1.jpg"
//                },
//            };
//        }

//        private IEnumerable<Category> GetCategories()
//        {
//            return new List<Category>
//            {
//                new Category
//                {
//                    CategoryId = 1,
//                    Name = "Ví",
//                    Code = "VI",
//                },
//                new Category
//                {
//                    CategoryId = 2,
//                    Name = "Thắt lưng",
//                    Code = "THL"
//                },
//                new Category
//                {
//                    CategoryId = 3,
//                    Name = "Túi",
//                    Code = "TI"
//                },
//                new Category
//                {
//                    CategoryId = 4,
//                    Name = "Tất",
//                    Code = "TT"
//                },
//                new Category
//                {
//                    CategoryId = 5,
//                    Name = "Cà vạt",
//                    Code = "CV"
//                },
//                new Category
//                {
//                    CategoryId = 6,
//                    Name ="Giày",
//                    Code = "GY"
//                }
//            };
//        }

//        private IEnumerable<ImageType> GetImageType()
//        {
//            return new List<ImageType>
//            {
//                new ImageType { Code = "FRONT", Name = "Mặt trước" },
//                new ImageType { Code = "BACK", Name = "Mặt sau" },
//                new ImageType { Code = "SIDE", Name = "Mặt bên" },
//                new ImageType { Code = "TOP", Name = "Nhìn từ trên" },
//                new ImageType { Code = "BOTTOM", Name = "Đế/Đáy" },
//                new ImageType { Code = "DETAIL", Name = "Chi tiết" }
//            };
//        }
//        private IEnumerable<ProductSize> GetProductSizes()
//        {
//            return new List<ProductSize>
//            {
//                new ProductSize { ProductVariantId = 1, SizeId = 5, StockQuantity = 10 }, // Giày VariantId = 1, size 39
//                new ProductSize { ProductVariantId = 1, SizeId = 6, StockQuantity = 15 }, // size 40
//                new ProductSize { ProductVariantId = 2, SizeId = 7, StockQuantity = 8 },  // size 41
//                new ProductSize { ProductVariantId = 2, SizeId = 8, StockQuantity = 12 }  // size 42
//            };
//        }

//        private IEnumerable<Size> GetSizes()
//        {
//            return new List<Size>
//            {
//                new Size { SizeType = "S" },
//                new Size { SizeType = "M" },
//                new Size { SizeType = "L" },
//                new Size { SizeType = "XL" },
//                new Size { SizeType = "39" },
//                new Size { SizeType = "40" },
//                new Size { SizeType = "41" },
//                new Size { SizeType = "42" }
//            };
//        }

//        private IEnumerable<PaymentMethod> GetPaymentMethods()
//        {
//            return new List<PaymentMethod>
//            {
//                    new PaymentMethod { Code = "COD", Name = "Trả tiền khi nhận hàng" },
//                    new PaymentMethod { Code = "ZP", Name = "Zalo Pay" }
//            };
//        }

//        private IEnumerable<Product> GetPrimaryProducts()
//        {
//            var now = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();

//            return new List<Product>
//            {
//                new Product
//                {
//                    Name = "Ví Ngang Nam",
//                    Description = "",
//                    Price = 500000,
//                    CategoryId = 1, // Ví
//                    IsAvailable = true,
//                    CreatedAt = now,
//                    UpdatedAt = now,
//                    Images = new List<ProductImage>
//                    {
//                        new ProductImage
//                        {
//                            Url = "/Images/VI/vi_black_front.jpg",
//                            ImageTypeId = (int)ImageTypeEnum.Front,
//                            IsPrimary = true,
//                            SortOrder = 1,
//                            CreatedAt = now,
//                            UpdatedAt = now
//                        }
//                    },
//                    Variants = new List<ProductVariant>
//                    {
//                        new ProductVariant
//                        {
//                            VariantCode = "WALL-BROWN",
//                            Color = "Nâu",
//                            CreatedAt = now,
//                            UpdatedAt = now,
//                            Images = new List<ProductImage>
//                            {
//                                  new ProductImage
//                                  {
//                                     Url = "/Images/VI/vi_brown_front.jpg",
//                                     ImageTypeId = (int)ImageTypeEnum.Front,
//                                     IsPrimary = true,
//                                     SortOrder = 1,
//                                     CreatedAt = now,
//                                     UpdatedAt = now
//                                  },
//                                  new ProductImage
//                                  {
//                                     Url = "/Images/VI/vi_brown_back.jpg",
//                                     ImageTypeId = (int)ImageTypeEnum.Back,
//                                     IsPrimary = false,
//                                     SortOrder = 2,
//                                     CreatedAt = now,
//                                     UpdatedAt = now
//                                  },
//                                  new ProductImage
//                                  {
//                                     Url = "/Images/VI/vi_brown_mat.jpg",
//                                     ImageTypeId = (int)ImageTypeEnum.Back,
//                                     IsPrimary = false,
//                                     SortOrder = 3,
//                                     CreatedAt = now,
//                                     UpdatedAt = now
//                                  }
//                            }
//                        },
//                    }
//                },

//                new Product
//                {
//                    Name = "Dây lưng lẻ da bò dập vân xỏ kim Aristino",
//                    Description = "",
//                    Price = 700000,
//                    CategoryId = 2,
//                    IsAvailable = true,
//                    CreatedAt = now,
//                    UpdatedAt = now,
//                    Images = new List<ProductImage>
//                    {
//                        new ProductImage
//                        {
//                            Url = "/Images/TTL/thl_black_f.jpg",
//                            ImageTypeId = (int)ImageTypeEnum.Front,
//                            IsPrimary = true,
//                            SortOrder = 1,
//                            CreatedAt = now,
//                            UpdatedAt = now
//                        }
//                    },
//                    Variants = new List<ProductVariant>
//                    {
//                        new ProductVariant
//                        {
//                            VariantCode = "BELT-BROWN",
//                            Color = "Nâu",
//                            CreatedAt = now,
//                            UpdatedAt = now,
//                            Images =  new List<ProductImage>
//                            {
//                                  new ProductImage
//                                  {
//                                     Url = "/Images/THL/thl_brown_f.jpg",
//                                     ImageTypeId = (int)ImageTypeEnum.Front,
//                                     IsPrimary = true,
//                                     SortOrder = 1,
//                                     CreatedAt = now,
//                                     UpdatedAt = now
//                                  },
//                                  new ProductImage
//                                  {
//                                     Url = "/Images/THL/thl_brown_b.jpg",
//                                     ImageTypeId = (int)ImageTypeEnum.Back,
//                                     IsPrimary = false,
//                                     SortOrder = 2,
//                                     CreatedAt = now,
//                                     UpdatedAt = now
//                                  }
//                            }
//                        }
//                    }
//                },

//                new Product
//                {
//                    Name = "Dây lưng lẻ da bò dập vân Aristino",
//                    Description = "",
//                    Price = 700000,
//                    CategoryId = 2,
//                    IsAvailable = true,
//                    CreatedAt = now,
//                    UpdatedAt = now,
//                    Images = new List<ProductImage>
//                    {
//                        new ProductImage
//                        {
//                            Url = "/Images/TTL/thl_black_hole_f.jpg",
//                            ImageTypeId = (int)ImageTypeEnum.Front,
//                            IsPrimary = true,
//                            SortOrder = 1,
//                            CreatedAt = now,
//                            UpdatedAt = now
//                        }                    
//                    },
//                    Variants = new List<ProductVariant>
//                    {
//                        new ProductVariant
//                        {
//                            VariantCode = "BELT-BROWN-HOLE",
//                            Color = "Nâu",
//                            CreatedAt = now,
//                            UpdatedAt = now,
//                            Images =  new List<ProductImage>
//                            {
//                                    new ProductImage
//                                    {
//                                        Url = "/Images/THL/thl_brown_hole_f.jpg",
//                                        ImageTypeId = (int)ImageTypeEnum.Front,
//                                        IsPrimary = true,
//                                        SortOrder = 1,
//                                        CreatedAt = now,
//                                        UpdatedAt = now
//                                    }
//                            }
//                        }
//                    }
//                },


//                 new Product
//                {
//                    Name = "Cà Vạt Nam Kẻ Đan Lát Bản To 7x3.8cm Aristino",
//                    Description = "",
//                    Price = 500000,
//                    CategoryId = 5,
//                    IsAvailable = true,
//                    CreatedAt = now,
//                    UpdatedAt = now,
//                    Images = new List<ProductImage>
//                    {
//                        new ProductImage
//                        {
//                            Url = "/Images/CV/cv_black_smooth_f.jpg",
//                            ImageTypeId = (int)ImageTypeEnum.Front,
//                            IsPrimary = true,
//                            SortOrder = 1,
//                            CreatedAt = now,
//                            UpdatedAt = now
//                        }
//                    },
//                },

//                new Product
//                {
//                    Name = "Giày Da Moca Nam Aristino Business",
//                    Description = "",
//                    Price = 500000,
//                    CategoryId = 6,
//                    IsAvailable = true,
//                    CreatedAt = now,
//                    UpdatedAt = now,
//                    Images = new List<ProductImage>
//                    {
//                        new ProductImage
//                        {
//                            Url = "/Images/GY/gy_black_smooth_f.jpg",
//                            ImageTypeId = (int)ImageTypeEnum.Front,
//                            IsPrimary = true,
//                            SortOrder = 1,
//                            CreatedAt = now,
//                            UpdatedAt = now
//                        }
//                    },
//                },

//                new Product
//                {
//                    Name = "Giày Da Moca Nam Aristino Business",
//                    Description = "",
//                    Price = 500000,
//                    CategoryId = 6,
//                    IsAvailable = true,
//                    CreatedAt = now,
//                    UpdatedAt = now,
//                    Images = new List<ProductImage>
//                    {
//                        new ProductImage
//                        {
//                            Url = "/Images/GY/gy_black_smooth_f.jpg",
//                            ImageTypeId = (int)ImageTypeEnum.Front,
//                            IsPrimary = true,
//                            SortOrder = 1,
//                            CreatedAt = now,
//                            UpdatedAt = now
//                        }
//                    },
//                },
//            };
//        }
//    }
//}
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Entities;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Seeders
{
    public class Seeder(DemoDbContext dbContext)
    {
        private readonly DemoDbContext _db = dbContext;
        private readonly Random _rnd = new Random();

        public async Task Seed()
        {
            if (!await _db.Database.CanConnectAsync()) return;

            await SeedRolesAsync();
            await SeedImageTypesAsync();
            await SeedCategoriesAsync();
            await SeedSizesAsync();
            await SeedPaymentMethodsAsync();

            // Products + Variants + Images + ProductSizes
            await SeedProductsAsync();

            // Users + Cart + Orders
            var users = await SeedUsersAsync();

            await SeedCartsAsync(users);
            await SeedOrdersAsync(users);
        }

        #region Helpers
        private static int NowUnix() => (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        private static int DaysAgoUnix(int days) => (int)DateTimeOffset.UtcNow.AddDays(-days).ToUnixTimeSeconds();

        private static readonly string[] BeltSizes = ["S", "M", "L", "XL"];
        private static readonly string[] SockSizes = ["38", "39", "40", "41", "42"]; // Tất (option B)
        private static readonly string[] ShoeSizes = ["39", "40", "41", "42"];

        private async Task<int?> GetSizeIdByTypeAsync(string sizeType)
            => (await _db.Sizes.AsNoTracking().FirstOrDefaultAsync(s => s.SizeType == sizeType))?.Id;

        private async Task<int[]> GetSizeIdsByTypesAsync(IEnumerable<string> types)
        {
            var set = new HashSet<string>(types);
            return await _db.Sizes.Where(s => set.Contains(s.SizeType)).Select(s => s.Id).ToArrayAsync();
        }

        private async Task<PaymentMethod> GetPaymentMethodAsync(string code)
            => await _db.PaymentMethods.FirstAsync(m => m.Code == code);

        private static bool CategoryHasSize(int categoryId) =>
            categoryId is 2 or 4 or 6; // Thắt lưng (2), Tất (4), Giày (6)

        private static string RandomVietnamAddress(Random rnd)
        {
            var provinces = new[]
            {
                "Hà Nội","TP. Hồ Chí Minh","Đà Nẵng","Hải Phòng","Cần Thơ","Đồng Nai","Bình Dương","Khánh Hòa","Lâm Đồng","Quảng Ninh"
            };
            var streets = new[] { "Nguyễn Trãi", "Trần Hưng Đạo", "Lê Lợi", "Hai Bà Trưng", "Điện Biên Phủ", "Phạm Văn Đồng" };
            return $"{rnd.Next(1, 200)} {streets[rnd.Next(streets.Length)]}, {provinces[rnd.Next(provinces.Length)]}";
        }
        #endregion

        #region Seed primitives
        private async Task SeedRolesAsync()
        {
            if (!await _db.Roles.AnyAsync())
            {
                _db.Roles.AddRange(
                    new Role { RoleId = 1, Name = "Admin" },
                    new Role { RoleId = 2, Name = "User" }
                );
                await _db.SaveChangesAsync();
            }
        }

        private async Task SeedImageTypesAsync()
        {
            if (!await _db.ImageTypes.AnyAsync())
            {
                _db.ImageTypes.AddRange(
                    //new ImageType { Id = (int)ImageTypeEnum.Front, Code = "FRONT", Name = "Mặt trước" },
                    //new ImageType { Id = (int)ImageTypeEnum.Back, Code = "BACK", Name = "Mặt sau" },
                    //new ImageType { Id = (int)ImageTypeEnum.Side, Code = "SIDE", Name = "Mặt bên" },
                    //new ImageType { Id = (int)ImageTypeEnum.Top, Code = "TOP", Name = "Nhìn từ trên" },
                    //new ImageType { Id = (int)ImageTypeEnum.Bottom, Code = "BOTTOM", Name = "Đế/Đáy" },
                    //new ImageType { Id = (int)ImageTypeEnum.Detail, Code = "DETAIL", Name = "Chi tiết" }
                    new ImageType { Code = "FRONT", Name = "Mặt trước" },
                    new ImageType { Code = "BACK", Name = "Mặt sau" },
                    new ImageType { Code = "SIDE", Name = "Mặt bên" },
                    new ImageType { Code = "TOP", Name = "Nhìn từ trên" },
                    new ImageType { Code = "BOTTOM", Name = "Đế/Đáy" },
                    new ImageType { Code = "DETAIL", Name = "Chi tiết" }
                );
                await _db.SaveChangesAsync();
            }
        }

        private async Task SeedCategoriesAsync()
        {
            var now = NowUnix();
            // 6 categories, fixed CategoryId
            var categories = new[]
            {
                new Category { CategoryId = 1, Name = "Ví",        Code = "VI",  CreatedAt = now, UpdatedAt = now, IsDeleted = false },
                new Category { CategoryId = 2, Name = "Thắt lưng", Code = "THL", CreatedAt = now, UpdatedAt = now, IsDeleted = false },
                new Category { CategoryId = 3, Name = "Túi",       Code = "TI",  CreatedAt = now, UpdatedAt = now, IsDeleted = false },
                new Category { CategoryId = 4, Name = "Tất",       Code = "TT",  CreatedAt = now, UpdatedAt = now, IsDeleted = false },
                new Category { CategoryId = 5, Name = "Cà vạt",    Code = "CV",  CreatedAt = now, UpdatedAt = now, IsDeleted = false },
                new Category { CategoryId = 6, Name = "Giày",      Code = "GY",  CreatedAt = now, UpdatedAt = now, IsDeleted = false },
            };

            foreach (var c in categories)
            {
                if (!await _db.Categories.AnyAsync(x => x.CategoryId == c.CategoryId))
                    _db.Categories.Add(c);
            }

            await _db.SaveChangesAsync();

            // Optional: ensure one CategoryImage each (skip if exists)
            foreach (var c in categories)
            {
                if (!await _db.CategoryImages.AnyAsync(ci => ci.CategoryId == c.CategoryId))
                {
                    _db.CategoryImages.Add(new CategoryImage
                    {
                        CategoryId = c.CategoryId,
                        Url = $"/Images/Categories/{c.Code.ToLower()}_cover.jpg"
                    });
                }
            }
            await _db.SaveChangesAsync();
        }

        private async Task SeedSizesAsync()
        {
            // Ensure S, M, L, XL, 38, 39, 40, 41, 42 exist
            var needed = new HashSet<string>(["S", "M", "L", "XL", "38", "39", "40", "41", "42"]);
            var existing = await _db.Sizes.Select(s => s.SizeType).ToListAsync();
            var toAdd = needed.Except(existing).Select(t => new Size { SizeType = t });
            if (toAdd.Any())
            {
                _db.Sizes.AddRange(toAdd);
                await _db.SaveChangesAsync();
            }
        }

        private async Task SeedPaymentMethodsAsync()
        {
            var want = new[]
            {
                new PaymentMethod { Code = "COD", Name = "Trả tiền khi nhận hàng", IsActive = true },
                new PaymentMethod { Code = "ZP",  Name = "Zalo Pay",               IsActive = true }
            };

            foreach (var m in want)
            {
                var exists = await _db.PaymentMethods.AnyAsync(x => x.Code == m.Code);
                if (!exists) _db.PaymentMethods.Add(m);
            }
            await _db.SaveChangesAsync();
        }
        #endregion

        #region Seed products/variants/images/sizes
        private async Task SeedProductsAsync()
        {
            if (await _db.Products.AnyAsync()) return; // tránh tạo trùng nếu bạn đã có dữ liệu

            var now = NowUnix();

            // Factory: tạo 4+ products cho 6 category
            var allProducts = new List<Product>();

            // 1. Ví (no size)
            allProducts.AddRange(MakeProducts(
                categoryId: 1,
                priceBase: 450_000m,
                names: new[]
                {
                    "Ví Ngang Da Bò Trơn",
                    "Ví Dài Nam Da Mềm",
                    "Ví Mini Da Saffiano",
                    "Ví Cầm Tay Khóa Kéo",
                    "Ví Da Bò Dập Vân"
                },
                variantDefs: new[] { "Đen", "Nâu", "Xanh navy" },
                hasSize: false));

            // 2. Thắt lưng (S/M/L/XL)
            allProducts.AddRange(MakeProducts(
                categoryId: 2,
                priceBase: 650_000m,
                names: new[]
                {
                    "Thắt Lưng Da Bò Mặt Khóa Kim",
                    "Thắt Lưng Da Trơn Mặt Tự Động",
                    "Thắt Lưng Dập Vân",
                    "Thắt Lưng Da Lộn",
                    "Thắt Lưng Bản Nhỏ"
                },
                variantDefs: new[] { "Đen", "Nâu" },
                hasSize: true,
                sizeTypes: BeltSizes));

            // 3. Túi (no size)
            allProducts.AddRange(MakeProducts(
                categoryId: 3,
                priceBase: 890_000m,
                names: new[]
                {
                    "Túi Đeo Chéo Da Bò",
                    "Túi Tote Da Mềm",
                    "Túi Messenger Công Sở",
                    "Túi Mini Đựng Ví",
                    "Túi Clutch Dự Tiệc"
                },
                variantDefs: new[] { "Đen", "Nâu sẫm" },
                hasSize: false));

            // 4. Tất (38–42)
            allProducts.AddRange(MakeProducts(
                categoryId: 4,
                priceBase: 59_000m,
                names: new[]
                {
                    "Tất Cổ Trung Cotton",
                    "Tất Cổ Ngắn Thoáng Khí",
                    "Tất Khử Mùi Kháng Khuẩn",
                    "Tất Thể Thao Dày",
                    "Tất Không Viền"
                },
                variantDefs: new[] { "Đen", "Trắng", "Xám" },
                hasSize: true,
                sizeTypes: SockSizes));

            // 5. Cà vạt (no size)
            allProducts.AddRange(MakeProducts(
                categoryId: 5,
                priceBase: 220_000m,
                names: new[]
                {
                    "Cà Vạt Trơn Bản Nhỏ",
                    "Cà Vạt Kẻ Chéo",
                    "Cà Vạt Họa Tiết Viền",
                    "Cà Vạt Sọc Mảnh",
                    "Cà Vạt Satin Mịn"
                },
                variantDefs: new[] { "Đen", "Xanh navy", "Xám" },
                hasSize: false));

            // 6. Giày (39–42)
            allProducts.AddRange(MakeProducts(
                categoryId: 6,
                priceBase: 990_000m,
                names: new[]
                {
                    "Giày Derby Da Bò",
                    "Giày Oxford Trơn",
                    "Giày Moca Business",
                    "Giày Sneaker Da",
                    "Giày Loafer Penny"
                },
                variantDefs: new[] { "Đen", "Nâu", "Tan" },
                hasSize: true,
                sizeTypes: ShoeSizes));

            _db.Products.AddRange(allProducts);
            await _db.SaveChangesAsync();

            // Attach ProductSizes for variants that need sizes
            // (MakeProducts đã tạo sẵn ProductVariant.Sizes nếu hasSize = true, nên chỉ cần SaveChanges là đủ)
        }

        private IEnumerable<Product> MakeProducts(
            int categoryId,
            decimal priceBase,
            IEnumerable<string> names,
            IEnumerable<string> variantDefs,
            bool hasSize,
            IEnumerable<string>? sizeTypes = null)
        {
            var now = NowUnix();
            var result = new List<Product>();

            foreach (var name in names.Take(5)) // đảm bảo >=4, ở đây tạo 5
            {
                var p = new Product
                {
                    Name = name,
                    Description = "",
                    Price = priceBase + _rnd.Next(0, 4) * 50_000m,
                    CategoryId = categoryId,
                    IsAvailable = true,
                    CreatedAt = now,
                    UpdatedAt = now,
                    Images = new List<ProductImage>
                    {
                        new ProductImage
                        {
                            Url = $"/Images/{CodeOfCategory(categoryId)}/{Slug(name)}_front.jpg",
                            ImageTypeId = (int)ImageTypeEnum.Front,
                            IsPrimary = true,
                            SortOrder = 1,
                            CreatedAt = now,
                            UpdatedAt = now
                        }
                    },
                    Variants = new List<ProductVariant>()
                };

                // 1–3 variants
                var variantsToTake = _rnd.Next(1, 4);
                foreach (var color in variantDefs.Take(variantsToTake))
                {
                    var v = new ProductVariant
                    {
                        VariantCode = $"{ShortCode(name)}-{color}".ToUpper().Replace(" ", ""),
                        Color = color,
                        CreatedAt = now,
                        UpdatedAt = now,
                        IsAvailable = true,
                        Images = new List<ProductImage>
                        {
                            new ProductImage
                            {
                                Url = $"/Images/{CodeOfCategory(categoryId)}/{Slug(name)}_{Slug(color)}_front.jpg",
                                ImageTypeId = (int)ImageTypeEnum.Front,
                                IsPrimary = true,
                                SortOrder = 1,
                                CreatedAt = now,
                                UpdatedAt = now
                            }
                        }
                    };

                    if (hasSize && sizeTypes is not null && sizeTypes.Any())
                    {
                        v.Sizes = new List<ProductSize>();
                        foreach (var st in sizeTypes)
                        {
                            // ProductSize sẽ gắn SizeId sau khi SaveChanges? Không cần, EF cho attach trực tiếp SizeId bằng cách lookup trước đó.
                            // Tuy nhiên chúng ta chưa có SizeId tại đây → dùng lazy set sau Save? Đơn giản hơn: map SizeType -> Id ngay bây giờ bằng dictionary cache.
                            // Để giữ đơn giản, tạm để v.Sizes tạo sau khi Product được AddRange.
                        }
                    }

                    p.Variants.Add(v);
                }

                // Sau khi tạo v.Sizes rỗng ở trên, ta sẽ gắn SizeId ngay tại đây (đã có Sizes trong DB)
                if (hasSize)
                {
                    var sizeTypeArray = sizeTypes?.ToArray() ?? Array.Empty<string>();
                    // Build ProductSize entries for each variant
                    foreach (var v in p.Variants)
                    {
                        v.Sizes ??= new List<ProductSize>();
                        foreach (var st in sizeTypeArray)
                        {
                            // Tồn tại Size trong DB nhờ SeedSizesAsync
                            var sizeId = _db.Sizes.AsNoTracking().First(s => s.SizeType == st).Id;
                            v.Sizes.Add(new ProductSize
                            {
                                SizeId = sizeId,
                                StockQuantity = _rnd.Next(5, 21)
                            });
                        }
                    }
                }

                result.Add(p);
            }

            return result;
        }

        private static string CodeOfCategory(int categoryId) => categoryId switch
        {
            1 => "VI",
            2 => "THL",
            3 => "TI",
            4 => "TT",
            5 => "CV",
            6 => "GY",
            _ => "CAT"
        };

        private static string Slug(string s)
        {
            var x = s.ToLowerInvariant();
            // very light slug
            var arr = x.Select(ch => char.IsLetterOrDigit(ch) ? ch : '-').ToArray();
            return new string(arr).Trim('-');
        }

        private static string ShortCode(string name)
        {
            var words = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return string.Join("", words.Select(w => w[0]));
        }
        #endregion

        #region Seed Users
        private async Task<List<User>> SeedUsersAsync()
        {
            var users = new List<User>();
            var adminEmail = "admin@example.com";
            var admin = await _db.Users.FirstOrDefaultAsync(u => u.Email == adminEmail);
            if (admin is null)
            {
                admin = new User
                {
                    RoleId = 1,
                    Name = "System Admin",
                    Email = adminEmail,
                    Password = "admin123",
                    Address = RandomVietnamAddress(_rnd),
                    Phone = "0909999999",
                    CreatedDate = DateTime.UtcNow.AddDays(-30),
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true
                };
                _db.Users.Add(admin);
                users.Add(admin);
            }
            else
            {
                users.Add(admin);
            }
            var want = new[]
            {
                new { Name = "Minh Nguyen", Email = "user1@example.com", Phone = "0900000001" },
                new { Name = "Lan Tran",    Email = "user2@example.com", Phone = "0900000002" },
                new { Name = "Khoa Le",     Email = "user3@example.com", Phone = "0900000003" }
            };

            foreach (var w in want)
            {
                var exists = await _db.Users.FirstOrDefaultAsync(u => u.Email == w.Email);
                if (exists is null)
                {
                    var u = new User
                    {
                        RoleId = 2, // User
                        Name = w.Name,
                        Email = w.Email,
                        Password = "123456",
                        Address = RandomVietnamAddress(_rnd),
                        Phone = w.Phone,
                        CreatedDate = DateTime.UtcNow.AddDays(-_rnd.Next(10, 60)),
                        UpdatedDate = DateTime.UtcNow
                    };
                    _db.Users.Add(u);
                    users.Add(u);
                }
                else
                {
                    users.Add(exists);
                }
            }

            await _db.SaveChangesAsync();
            return users;
        }
        #endregion

        #region Seed Carts
        private async Task SeedCartsAsync(IEnumerable<User> users)
        {
            // Chỉ add CartItems cho các category có size (2,4,6) để đáp ứng FK SizeId NOT NULL
            var sizedVariantIds = await _db.ProductVariants
                .Where(v => v.Product.CategoryId == 2 || v.Product.CategoryId == 4 || v.Product.CategoryId == 6)
                .Select(v => new { v.Id, v.Product.CategoryId })
                .ToListAsync();

            var sizeMap = new Dictionary<int, int[]>(); // CategoryId -> SizeIds
            sizeMap[2] = await GetSizeIdsByTypesAsync(BeltSizes);
            sizeMap[4] = await GetSizeIdsByTypesAsync(SockSizes);
            sizeMap[6] = await GetSizeIdsByTypesAsync(ShoeSizes);

            foreach (var user in users)
            {
                // Ensure a cart
                var cart = await _db.Carts.FirstOrDefaultAsync(c => c.UsersId == user.Id);
                if (cart is null)
                {
                    cart = new Cart { UsersId = user.Id };
                    _db.Carts.Add(cart);
                    await _db.SaveChangesAsync();
                }

                // If cart already has items, skip adding more to avoid duplication
                if (await _db.CartItems.AnyAsync(ci => ci.CartId == cart.Id)) continue;

                var itemsToAdd = _rnd.Next(2, 6);
                for (int i = 0; i < itemsToAdd; i++)
                {
                    var pick = sizedVariantIds[_rnd.Next(sizedVariantIds.Count)];
                    var sizeIds = sizeMap[pick.CategoryId.Value];
                    var sizeId = sizeIds[_rnd.Next(sizeIds.Length)];

                    _db.CartItems.Add(new CartItem
                    {
                        CartId = cart.Id,
                        ProductVariantId = pick.Id,
                        SizeId = sizeId,
                        Quantity = _rnd.Next(1, 3)
                    });
                }
                await _db.SaveChangesAsync();
            }
        }
        #endregion

        #region Seed Orders + Payments + Shippings
        private async Task SeedOrdersAsync(IEnumerable<User> users)
        {
            var cod = await GetPaymentMethodAsync("COD");
            var zp = await GetPaymentMethodAsync("ZP");

            // pick some variants (prefer categories with size for simplicity)
            var allVariants = await _db.ProductVariants
                .Include(v => v.Product)
                .ToListAsync();

            // sizeIds per category
            var beltSizeIds = await GetSizeIdsByTypesAsync(BeltSizes);
            var sockSizeIds = await GetSizeIdsByTypesAsync(SockSizes);
            var shoeSizeIds = await GetSizeIdsByTypesAsync(ShoeSizes);

            foreach (var u in users)
            {
                // Nếu đã có 5+ orders thì bỏ qua
                var existingCount = await _db.Orders.CountAsync(o => o.UserId == u.Id);
                var need = Math.Max(0, 5 - existingCount);
                if (need <= 0) continue;

                for (int k = 0; k < need; k++)
                {
                    // Random order items: 1–3 items
                    var orderItems = new List<OrderItem>();
                    var itemCount = _rnd.Next(1, 4);

                    for (int i = 0; i < itemCount; i++)
                    {
                        var v = allVariants[_rnd.Next(allVariants.Count)];
                        int? sizeId = null;

                        // assign size if category requires it
                        if (v.Product.CategoryId == 2)
                            sizeId = beltSizeIds[_rnd.Next(beltSizeIds.Length)];
                        else if (v.Product.CategoryId == 4)
                            sizeId = sockSizeIds[_rnd.Next(sockSizeIds.Length)];
                        else if (v.Product.CategoryId == 6)
                            sizeId = shoeSizeIds[_rnd.Next(shoeSizeIds.Length)];
                        else
                        {
                            // Category without size: schema cho phép SizeId = null trên OrderItem
                            sizeId = null;
                        }

                        orderItems.Add(new OrderItem
                        {
                            ProductVariantId = v.Id,
                            Quantity = _rnd.Next(1, 4),
                            SizeId = sizeId,
                            SizeType = sizeId.HasValue ? (await _db.Sizes.FindAsync(sizeId.Value))?.SizeType : null
                        });
                    }

                    // Tính tổng tiền từ giá Product * qty
                    decimal total = 0m;
                    foreach (var it in orderItems)
                    {
                        var variant = allVariants.First(v => v.Id == it.ProductVariantId);
                        total += (variant.Product.Price * it.Quantity);
                    }

                    // Decide statuses coherently
                    var orderStatus = PickOrderStatus();
                    var (paymentStatus, shippingStatus) = MapStatuses(orderStatus);

                    var createdAt = DaysAgoUnix(_rnd.Next(2, 30));
                    var updatedAt = NowUnix();

                    var order = new Order
                    {
                        UserId = u.Id,
                        CreatedDate = createdAt,
                        UpdatedDate = updatedAt,
                        Status = orderStatus.ToString(),
                        Total = total,
                        Items = orderItems
                    };
                    _db.Orders.Add(order);
                    await _db.SaveChangesAsync(); // cần Id cho FK Payment/Shipping

                    // Payment
                    var method = _rnd.NextDouble() < 0.5 ? cod : zp;
                    var processed = paymentStatus == PaymentStatusEnum.Pending ? (int?)null : DaysAgoUnix(_rnd.Next(1, 2));
                    _db.Payments.Add(new Payment
                    {
                        OrderId = order.Id,
                        Amount = total,
                        PaymentMethodId = method.Id,
                        Status = paymentStatus.ToString(),
                        CreatedDate = createdAt,
                        ProcessedDate = processed,
                        IsDeleted = false,
                        UpdatedDate = updatedAt
                    });

                    // Shipping
                    var start = createdAt;
                    int? finish = shippingStatus == ShippingStatusEnum.Delivered ? updatedAt : null;

                    _db.Shippings.Add(new Shipping
                    {
                        OrderId = order.Id,
                        StartAddress = RandomVietnamAddress(_rnd),
                        EndAddress = u.Address ?? RandomVietnamAddress(_rnd),
                        CreatedDate = createdAt,
                        StartDate = start,
                        FinishDate = finish,
                        Status = shippingStatus.ToString()
                    });

                    await _db.SaveChangesAsync();
                }
            }
        }

        private static OrderStatusEnum PickOrderStatus()
        {
            var options = new[]
            {
                OrderStatusEnum.Pending,
                OrderStatusEnum.Processing,
                OrderStatusEnum.Shipping,
                OrderStatusEnum.Delivered,
                OrderStatusEnum.Success,
                OrderStatusEnum.Cancelled,
                OrderStatusEnum.Failed,
                OrderStatusEnum.Expired
            };
            // Weighting nhẹ để nhiều đơn "Delivered/Success" hơn
            var weighted = new[]
            {
                OrderStatusEnum.Pending,
                OrderStatusEnum.Processing,
                OrderStatusEnum.Shipping,
                OrderStatusEnum.Delivered,
                OrderStatusEnum.Delivered,
                OrderStatusEnum.Success,
                OrderStatusEnum.Success,
                OrderStatusEnum.Cancelled
            };
            var rnd = new Random();
            return weighted[rnd.Next(weighted.Length)];
        }

        private static (PaymentStatusEnum payment, ShippingStatusEnum shipping) MapStatuses(OrderStatusEnum order)
        {
            return order switch
            {
                OrderStatusEnum.Delivered or OrderStatusEnum.Success
                    => (PaymentStatusEnum.Success, ShippingStatusEnum.Delivered),

                OrderStatusEnum.Shipping
                    => (PaymentStatusEnum.Pending, ShippingStatusEnum.Shipping),

                OrderStatusEnum.Processing or OrderStatusEnum.Pending
                    => (PaymentStatusEnum.Pending, ShippingStatusEnum.Pending),

                OrderStatusEnum.Cancelled or OrderStatusEnum.Failed or OrderStatusEnum.Expired
                    => (PaymentStatusEnum.Cancelled, ShippingStatusEnum.Failed),

                _ => (PaymentStatusEnum.Pending, ShippingStatusEnum.Pending)
            };
        }
        #endregion
    }
}
