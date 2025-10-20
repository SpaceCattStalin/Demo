using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public DateTime CreatedDate { get; set; }

    public string ImageUrl { get; set; } = null!;

    public DateTime? UpdatedDate { get; set; }

    public bool IsAvailable { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
