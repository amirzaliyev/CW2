using System;
using System.Collections.Generic;

namespace CW2.DAL.Entities;

public partial class Customer
{
    public long CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    public byte[]? ProfilePic { get; set; }

    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? PostalCode { get; set; }

    public string? Street { get; set; }

    public string? BuildingNo { get; set; }

    public string? FlatNo { get; set; }

    public string? City { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public bool AcceptsMarketing { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
}
