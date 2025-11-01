using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Order
{
    public int Id { get; set; }

    public decimal Total { get; set; }

    public string Status { get; set; } = null!;

    public int CreatedDate { get; set; }

    public int? UpdatedDate { get; set; }

    public int UserId { get; set; }


    //public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

    public virtual List<Payment> Payments { get; set; } = new List<Payment>();

    public virtual List<Shipping> Shippings { get; set; } = new List<Shipping>();

    public virtual User Users { get; set; } = null!;
}
