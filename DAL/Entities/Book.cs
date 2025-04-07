using System;
using System.Collections.Generic;

namespace CW2.DAL.Entities;

public partial class Book
{
    public string Isbn { get; set; } = null!;

    public string Title { get; set; } = null!;

    public DateOnly? PublicationDate { get; set; }

    public int? Edition { get; set; }

    public int AvailableQuantity { get; set; }

    public decimal? Price { get; set; }

    public int? Author { get; set; }

    public int? Publisher { get; set; }

    public virtual Author? AuthorNavigation { get; set; }

    public virtual ICollection<OrdersDetail> OrdersDetails { get; set; } = new List<OrdersDetail>();

    public virtual Publisher? PublisherNavigation { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
