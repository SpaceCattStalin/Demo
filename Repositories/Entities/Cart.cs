using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Cart
{
    public int Id { get; set; }

    public int UsersId { get; set; }

    //public int ProductVariantId { get; set; }

    //public int Quantity { get; set; }

    //public decimal Amount { get; set; }

    //public int? OrdersId { get; set; }

    //public virtual Order? Orders { get; set; }

    public List<CartItem> Items { get; set; }
    public virtual User Users { get; set; } = null!;
}
