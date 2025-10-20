using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class DiscountCode
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Value { get; set; }

    public decimal MinimumAmount { get; set; }

    public DateTime ExpiredDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<UsersDiscountCode> UsersDiscountCodes { get; set; } = new List<UsersDiscountCode>();
}
