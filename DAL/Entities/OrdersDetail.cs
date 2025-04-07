using System;
using System.Collections.Generic;

namespace CW2.DAL.Entities;

public partial class OrdersDetail
{
    public long OrderId { get; set; }

    public string BookId { get; set; } = null!;

    public int Amount { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
