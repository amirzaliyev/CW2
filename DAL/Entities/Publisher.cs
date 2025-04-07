﻿using System;
using System.Collections.Generic;

namespace CW2.DAL.Entities;

public partial class Publisher
{
    public int PublisherId { get; set; }

    public string PublisherName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
