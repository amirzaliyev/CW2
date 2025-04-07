using System;
using System.Collections.Generic;

namespace CW2.DAL.Entities;

public partial class Discount
{
    public long DiscountId { get; set; }

    public string? DiscountName { get; set; }

    public decimal Value { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
