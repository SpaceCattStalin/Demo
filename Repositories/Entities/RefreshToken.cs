using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class RefreshToken
{
    public int Id { get; set; }

    public string Token { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime ExpiredDate { get; set; }

    public bool IsActive { get; set; }

    public int UsersId { get; set; }

    public virtual User Users { get; set; } = null!;
}
