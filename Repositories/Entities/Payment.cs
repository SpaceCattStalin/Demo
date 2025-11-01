using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Payment
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int PaymentMethodId { get; set; }
    public virtual PaymentMethod Method { get; set; }

    public string Status { get; set; } = null!;
    public int? ProcessedDate { get; set; }
    public int CreatedDate { get; set; }
    public int? UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
    public int OrderId { get; set; }
    public virtual Order Order { get; set; } = null!;
}
