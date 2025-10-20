using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Shipping
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public string StartAddress { get; set; } = null!;

    public string EndAddress { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime? FinishDate { get; set; }

    public virtual Order IdNavigation { get; set; } = null!;
}
