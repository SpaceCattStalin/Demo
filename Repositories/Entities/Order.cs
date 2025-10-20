using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Order
{
    public int Id { get; set; }

    public decimal Total { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int UsersId { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Payment? Payment { get; set; }

    public virtual Shipping? Shipping { get; set; }

    public virtual User Users { get; set; } = null!;
}
