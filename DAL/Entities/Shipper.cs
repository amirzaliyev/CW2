using System;
using System.Collections.Generic;

namespace CW2.DAL.Entities;

public partial class Shipper
{
    public long ShipperId { get; set; }

    public string ShipperName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
