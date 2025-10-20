using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class UsersDiscountCode
{
    public int UsersId { get; set; }

    public int DiscountCodeId { get; set; }

    public bool IsActive { get; set; }

    public virtual DiscountCode DiscountCode { get; set; } = null!;

    public virtual User Users { get; set; } = null!;
}
