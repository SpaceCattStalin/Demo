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

                if (!dbContext.Categories.Any())
                {
                    var categories = GetCategories();
                    dbContext.Categories.AddRange(categories);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.ImageTypes.Any())
                {
                    var images = GetImageType();
                    dbContext.ImageTypes.AddRange(images);
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
                    RoleId = 2,
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
                    RoleId = 3,
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

        private IEnumerable<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category
                {
                    //Id = 1,
                    Name = "Ví",
                    Code = "VI",
                },
                new Category
                {
                    //Id = 2,
                    Name = "Thắt lưng",
                    Code = "THL"
                },
                new Category
                {
                    //Id = 3,
                    Name = "Túi",
                    Code = "TI"
                },
                new Category
                {
                    //Id = 4,
                    Name = "Tất",
                    Code = "TT"
                },
                new Category
                {
                    //Id = 5,
                    Name = "Cà vạt",
                    Code = "CV"
                },
                new Category
                {
                    //Id = 6,
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
                    StockQuantity = 50,
                    CategoryId = 6, // Ví
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
                        new ProductImage
                        {
                            Url = "/Images/VI/vi_black_back.jpg",
                            ImageTypeId = (int)ImageTypeEnum.Back,
                            IsPrimary = false,
                            SortOrder = 2,
                            CreatedAt = now,
                            UpdatedAt = now
                        },
                        new ProductImage
                        {
                            Url = "/Images/VI/vi_black_mat.jpg",
                            ImageTypeId = (int)ImageTypeEnum.Back,
                            IsPrimary = false,
                            SortOrder = 3,
                            CreatedAt = now,
                            UpdatedAt = now
                        }
                    },
                    Variants = new List<ProductVariant>
                    {
                        new ProductVariant
                        {
                            VariantCode = "WALL-BROWN",
                            Color = "NAU",
                            Size = "",
                            CreatedAt = now,
                            UpdatedAt = now,
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
                    StockQuantity = 30,
                    CategoryId = 7,
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
                        new ProductImage
                        {
                            Url = "/Images/TTL/thl_black_b.jpg",
                            ImageTypeId = (int)ImageTypeEnum.Front,
                            IsPrimary = false,
                            SortOrder = 2,
                            CreatedAt = now,
                            UpdatedAt = now
                        }
                    },
                    Variants = new List<ProductVariant>
                    {
                        new ProductVariant
                        {
                            VariantCode = "BELT-BROWN",
                            Color = "NAU",
                            Size = "L",
                            CreatedAt = now,
                            UpdatedAt = now,
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
                    StockQuantity = 30,
                    CategoryId = 7,
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
                        new ProductImage
                        {
                            Url = "/Images/TTL/thl_black_hole_b.jpg",
                            ImageTypeId = (int)ImageTypeEnum.Front,
                            IsPrimary = false,
                            SortOrder = 2,
                            CreatedAt = now,
                            UpdatedAt = now
                        }
                    },
                    Variants = new List<ProductVariant>
                    {
                        new ProductVariant
                        {
                            VariantCode = "BELT-BROWN-HOLE",
                            Color = "NAU",
                            Size = "L",
                            CreatedAt = now,
                            UpdatedAt = now,
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
                    StockQuantity = 50,
                    CategoryId = 10,
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
                        new ProductImage
                        {
                            Url = "/Images/CV/cv_black_smooth_b.jpg",
                            ImageTypeId = (int)ImageTypeEnum.Back,
                            IsPrimary = false,
                            SortOrder = 2,
                            CreatedAt = now,
                            UpdatedAt = now
                        },
                    },
                },

                new Product
                {
                    Name = "Giày Da Moca Nam Aristino Business",
                    Description = "",
                    Price = 500000,
                    StockQuantity = 50,
                    CategoryId = 11,
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
                        new ProductImage
                        {
                            Url = "/Images/CV/cv_black_smooth_b.jpg",
                            ImageTypeId = (int)ImageTypeEnum.Back,
                            IsPrimary = false,
                            SortOrder = 2,
                            CreatedAt = now,
                            UpdatedAt = now
                        },
                    },
                },

                new Product
                {
                    Name = "Giày Da Moca Nam Aristino Business",
                    Description = "",
                    Price = 500000,
                    StockQuantity = 50,
                    CategoryId = 11,
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
                        new ProductImage
                        {
                            Url = "/Images/CV/cv_black_smooth_b.jpg",
                            ImageTypeId = (int)ImageTypeEnum.Back,
                            IsPrimary = false,
                            SortOrder = 2,
                            CreatedAt = now,
                            UpdatedAt = now
                        },
                    },
                },
            };
        }
    }
}
