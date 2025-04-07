using System;
using System.Collections.Generic;

namespace CW2.DAL.Entities;

public partial class Review
{
    public int Id { get; set; }

    public string BookId { get; set; } = null!;

    public long CustomerId { get; set; }

    public int Review1 { get; set; }

    public DateOnly Date { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
