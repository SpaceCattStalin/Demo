using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Entities;
using Services.Utils;

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
                if (!dbContext.Sizes.Any())
                {
                    var sizes = GetSizes();
                    dbContext.Sizes.AddRange(sizes);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Categories.Any())
                {
                    var categories = GetCategories();
                    dbContext.Categories.AddRange(categories);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.CategoryImages.Any())
                {
                    var categoryImages = GetCategoryImages();
                    dbContext.CategoryImages.AddRange(categoryImages);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.ImageTypes.Any())
                {
                    var images = GetImageType();
                    dbContext.ImageTypes.AddRange(images);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.PaymentMethods.Any())
                {
                    var paymentMethods = GetPaymentMethods();
                    dbContext.PaymentMethods.AddRange(paymentMethods);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Products.Any())
                {
                    var products = GetPrimaryProducts();
                    dbContext.Products.AddRange(products);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.ProductSizes.Any())
                {
                    var productSizes = GetProductSizes();
                    dbContext.ProductSizes.AddRange(productSizes);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
        private IEnumerable<Role> GetPrimaryRoles()
        {
            return new List<Role>
            {
                new() { RoleId = 1,Name = "Admin" },
                new() { RoleId = 2, Name = "User" },
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

        private IEnumerable<CategoryImage> GetCategoryImages()
        {
            return new List<CategoryImage>
            {
                new CategoryImage
                {
                    CategoryId = 1,
                    Url = "/Images/Categories/vi_photo_shoot_1.jpg"
                },
                new CategoryImage
                {
                    CategoryId = 2,
                    Url = "/Images/Categories/that_lung_photo_shoot_1.jpg"
                },
                new CategoryImage
                {
                    CategoryId = 3,
                    Url = "/Images/Categories/tui_photo_shoot_1.jpg"
                },
                new CategoryImage
                {
                    CategoryId = 4,
                    Url = "/Images/Categories/tat_photo_shoot_1.jpg"
                },
                new CategoryImage
                {
                    CategoryId = 5,
                    Url = "/Images/Categories/ca_vat_photo_shoot_1.jpg"
                },
                new CategoryImage
                {
                    CategoryId = 6,
                    Url = "/Images/Categories/giay_photo_shoot_1.jpg"
                },
            };
        }

        private IEnumerable<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category
                {
                    CategoryId = 1,
                    Name = "Ví",
                    Code = "VI",
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Thắt lưng",
                    Code = "THL"
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Túi",
                    Code = "TI"
                },
                new Category
                {
                    CategoryId = 4,
                    Name = "Tất",
                    Code = "TT"
                },
                new Category
                {
                    CategoryId = 5,
                    Name = "Cà vạt",
                    Code = "CV"
                },
                new Category
                {
                    CategoryId = 6,
                    Name ="Giày",
                    Code = "GY"
                }
            };
        }

        private IEnumerable<ImageType> GetImageType()
        {
            return new List<ImageType>
            {
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
            };
        }
        private IEnumerable<ProductSize> GetProductSizes()
        {
            return new List<ProductSize>
            {
                new ProductSize { ProductVariantId = 1, SizeId = 5, StockQuantity = 10 }, // Giày VariantId = 1, size 39
                new ProductSize { ProductVariantId = 1, SizeId = 6, StockQuantity = 15 }, // size 40
                new ProductSize { ProductVariantId = 2, SizeId = 7, StockQuantity = 8 },  // size 41
                new ProductSize { ProductVariantId = 2, SizeId = 8, StockQuantity = 12 }  // size 42
            };
        }

        private IEnumerable<Size> GetSizes()
        {
            return new List<Size>
            {
                new Size { SizeType = "S" },
                new Size { SizeType = "M" },
                new Size { SizeType = "L" },
                new Size { SizeType = "XL" },
                new Size { SizeType = "39" },
                new Size { SizeType = "40" },
                new Size { SizeType = "41" },
                new Size { SizeType = "42" }
            };
        }

        private IEnumerable<PaymentMethod> GetPaymentMethods()
        {
            return new List<PaymentMethod>
            {
                    new PaymentMethod { Code = "COD", Name = "Trả tiền khi nhận hàng" },
                    new PaymentMethod { Code = "ZP", Name = "Zalo Pay" }
            };
        }

        private IEnumerable<Product> GetPrimaryProducts()
        {
            var now = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            return new List<Product>
            {
                new Product
                {
                    Name = "Ví Ngang Nam",
                    Description = "",
                    Price = 500000,
                    //StockQuantity = 50,
                    CategoryId = 1, // Ví
                    IsAvailable = true,
                    CreatedAt = now,
                    UpdatedAt = now,
                    Images = new List<ProductImage>
                    {
                        new ProductImage
                        {
                            Url = "/Images/VI/vi_black_front.jpg",
                            ImageTypeId = (int)ImageTypeEnum.Front,
                            IsPrimary = true,
                            SortOrder = 1,
                            CreatedAt = now,
                            UpdatedAt = now
                        },
                        //new ProductImage
                        //{
                        //    Url = "/Images/VI/vi_black_back.jpg",
                        //    ImageTypeId = (int)ImageTypeEnum.Back,
                        //    IsPrimary = false,
                        //    SortOrder = 2,
                        //    CreatedAt = now,
                        //    UpdatedAt = now
                        //},
                        //new ProductImage
                        //{
                        //    Url = "/Images/VI/vi_black_mat.jpg",
                        //    ImageTypeId = (int)ImageTypeEnum.Back,
                        //    IsPrimary = false,
                        //    SortOrder = 3,
                        //    CreatedAt = now,
                        //    UpdatedAt = now
                        //}
                    },
                    Variants = new List<ProductVariant>
                    {
                        new ProductVariant
                        {
                            VariantCode = "WALL-BROWN",
                            Color = "Nâu",
                            //Size = "",
                            CreatedAt = now,
                            UpdatedAt = now,
                            //StockQuantity = 10,
                            Images = new List<ProductImage>
                            {
                                  new ProductImage
                                  {
                                     Url = "/Images/VI/vi_brown_front.jpg",
                                     ImageTypeId = (int)ImageTypeEnum.Front,
                                     IsPrimary = true,
                                     SortOrder = 1,
                                     CreatedAt = now,
                                     UpdatedAt = now
                                  },
                                  new ProductImage
                                  {
                                     Url = "/Images/VI/vi_brown_back.jpg",
                                     ImageTypeId = (int)ImageTypeEnum.Back,
                                     IsPrimary = false,
                                     SortOrder = 2,
                                     CreatedAt = now,
                                     UpdatedAt = now
                                  },
                                  new ProductImage
                                  {
                                     Url = "/Images/VI/vi_brown_mat.jpg",
                                     ImageTypeId = (int)ImageTypeEnum.Back,
                                     IsPrimary = false,
                                     SortOrder = 3,
                                     CreatedAt = now,
                                     UpdatedAt = now
                                  }
                            }
                        },
                    }
                },

                new Product
                {
                    Name = "Dây lưng lẻ da bò dập vân xỏ kim Aristino",
                    Description = "",
                    Price = 700000,
                    //StockQuantity = 30,
                    CategoryId = 2,
                    IsAvailable = true,
                    CreatedAt = now,
                    UpdatedAt = now,
                    Images = new List<ProductImage>
                    {
                        new ProductImage
                        {
                            Url = "/Images/TTL/thl_black_f.jpg",
                            ImageTypeId = (int)ImageTypeEnum.Front,
                            IsPrimary = true,
                            SortOrder = 1,
                            CreatedAt = now,
                            UpdatedAt = now
                        },
                        //new ProductImage
                        //{
                        //    Url = "/Images/TTL/thl_black_b.jpg",
                        //    ImageTypeId = (int)ImageTypeEnum.Front,
                        //    IsPrimary = false,
                        //    SortOrder = 2,
                        //    CreatedAt = now,
                        //    UpdatedAt = now
                        //}
                    },
                    Variants = new List<ProductVariant>
                    {
                        new ProductVariant
                        {
                            VariantCode = "BELT-BROWN",
                            Color = "Nâu",
                            //Size = "L",
                            CreatedAt = now,
                            UpdatedAt = now,
                            //StockQuantity = 10,
                            Images =  new List<ProductImage>
                            {
                                  new ProductImage
                                  {
                                     Url = "/Images/THL/thl_brown_f.jpg",
                                     ImageTypeId = (int)ImageTypeEnum.Front,
                                     IsPrimary = true,
                                     SortOrder = 1,
                                     CreatedAt = now,
                                     UpdatedAt = now
                                  },
                                  new ProductImage
                                  {
                                     Url = "/Images/THL/thl_brown_b.jpg",
                                     ImageTypeId = (int)ImageTypeEnum.Back,
                                     IsPrimary = false,
                                     SortOrder = 2,
                                     CreatedAt = now,
                                     UpdatedAt = now
                                  }
                            }
                        }
                    }
                },

                new Product
                {
                    Name = "Dây lưng lẻ da bò dập vân Aristino",
                    Description = "",
                    Price = 700000,
                    //StockQuantity = 30,
                    CategoryId = 2,
                    IsAvailable = true,
                    CreatedAt = now,
                    UpdatedAt = now,
                    Images = new List<ProductImage>
                    {
                        new ProductImage
                        {
                            Url = "/Images/TTL/thl_black_hole_f.jpg",
                            ImageTypeId = (int)ImageTypeEnum.Front,
                            IsPrimary = true,
                            SortOrder = 1,
                            CreatedAt = now,
                            UpdatedAt = now
                        },
                        //new ProductImage
                        //{
                        //    Url = "/Images/TTL/thl_black_hole_b.jpg",
                        //    ImageTypeId = (int)ImageTypeEnum.Front,
                        //    IsPrimary = false,
                        //    SortOrder = 2,
                        //    CreatedAt = now,
                        //    UpdatedAt = now
                        //}
                    },
                    Variants = new List<ProductVariant>
                    {
                        new ProductVariant
                        {
                            VariantCode = "BELT-BROWN-HOLE",
                            Color = "Nâu",
                            //Size = "L",
                            CreatedAt = now,
                            UpdatedAt = now,
                            //StockQuantity = 10,
                            Images =  new List<ProductImage>
                            {
                                    new ProductImage
                                    {
                                        Url = "/Images/THL/thl_brown_hole_f.jpg",
                                        ImageTypeId = (int)ImageTypeEnum.Front,
                                        IsPrimary = true,
                                        SortOrder = 1,
                                        CreatedAt = now,
                                        UpdatedAt = now
                                    }
                            }
                        }
                    }
                },


                 new Product
                {
                    Name = "Cà Vạt Nam Kẻ Đan Lát Bản To 7x3.8cm Aristino",
                    Description = "",
                    Price = 500000,
                    //StockQuantity = 50,
                    CategoryId = 5,
                    IsAvailable = true,
                    CreatedAt = now,
                    UpdatedAt = now,
                    Images = new List<ProductImage>
                    {
                        new ProductImage
                        {
                            Url = "/Images/CV/cv_black_smooth_f.jpg",
                            ImageTypeId = (int)ImageTypeEnum.Front,
                            IsPrimary = true,
                            SortOrder = 1,
                            CreatedAt = now,
                            UpdatedAt = now
                        },
                        //new ProductImage
                        //{
                        //    Url = "/Images/CV/cv_black_smooth_b.jpg",
                        //    ImageTypeId = (int)ImageTypeEnum.Back,
                        //    IsPrimary = false,
                        //    SortOrder = 2,
                        //    CreatedAt = now,
                        //    UpdatedAt = now
                        //},
                    },
                },

                new Product
                {
                    Name = "Giày Da Moca Nam Aristino Business",
                    Description = "",
                    Price = 500000,
                    //StockQuantity = 50,
                    CategoryId = 6,
                    IsAvailable = true,
                    CreatedAt = now,
                    UpdatedAt = now,
                    Images = new List<ProductImage>
                    {
                        new ProductImage
                        {
                            Url = "/Images/GY/gy_black_smooth_f.jpg",
                            ImageTypeId = (int)ImageTypeEnum.Front,
                            IsPrimary = true,
                            SortOrder = 1,
                            CreatedAt = now,
                            UpdatedAt = now
                        },
                        //new ProductImage
                        //{
                        //    Url = "/Images/CV/cv_black_smooth_b.jpg",
                        //    ImageTypeId = (int)ImageTypeEnum.Back,
                        //    IsPrimary = false,
                        //    SortOrder = 2,
                        //    CreatedAt = now,
                        //    UpdatedAt = now
                        //},
                    },
                },

                new Product
                {
                    Name = "Giày Da Moca Nam Aristino Business",
                    Description = "",
                    Price = 500000,
                    //StockQuantity = 50,
                    CategoryId = 6,
                    IsAvailable = true,
                    CreatedAt = now,
                    UpdatedAt = now,
                    Images = new List<ProductImage>
                    {
                        new ProductImage
                        {
                            Url = "/Images/GY/gy_black_smooth_f.jpg",
                            ImageTypeId = (int)ImageTypeEnum.Front,
                            IsPrimary = true,
                            SortOrder = 1,
                            CreatedAt = now,
                            UpdatedAt = now
                        },
                        //new ProductImage
                        //{
                        //    Url = "/Images/CV/cv_black_smooth_b.jpg",
                        //    ImageTypeId = (int)ImageTypeEnum.Back,
                        //    IsPrimary = false,
                        //    SortOrder = 2,
                        //    CreatedAt = now,
                        //    UpdatedAt = now
                        //},
                    },
                },
            };
        }
    }
}
