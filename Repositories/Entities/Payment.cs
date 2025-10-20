using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Payment
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public string Type { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? ProcessedDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Order IdNavigation { get; set; } = null!;
}
