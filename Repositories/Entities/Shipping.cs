using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Shipping
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public string StartAddress { get; set; } = null!;

    public string EndAddress { get; set; } = null!;
    public int CreatedDate { get; set; }
    public int StartDate { get; set; }

    public int? FinishDate { get; set; }
    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;
}
