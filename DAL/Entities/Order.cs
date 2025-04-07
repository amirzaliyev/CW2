using System;
using System.Collections.Generic;

namespace CW2.DAL.Entities;

public partial class Order
{
    public long OrderId { get; set; }

    public long CustomerId { get; set; }

    public DateOnly Date { get; set; }

    public long? DiscountId { get; set; }

    public long Shipper { get; set; }

    public string State { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Discount? Discount { get; set; }

    public virtual ICollection<OrdersDetail> OrdersDetails { get; set; } = new List<OrdersDetail>();

    public virtual Shipper ShipperNavigation { get; set; } = null!;
}
