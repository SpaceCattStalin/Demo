using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    //public int StockQuantity { get; set; }

    //public DateTime CreatedDate { get; set; }

    //public DateTime? UpdatedDate { get; set; }

    public int CreatedAt { get; set; }
    public int UpdatedAt { get; set; }

    public bool IsAvailable { get; set; } = true;
    //public string? Color { get; set; }
    //public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public int? CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<ProductVariant> Variants { get; set; }
    public ICollection<ProductImage> Images { get; set; }
    //public ICollection<ProductSize> ProductSize { get; set; }
}
