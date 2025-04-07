using System;
using System.Collections.Generic;
using CW2.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CW2.DAL.EF;

public partial class BookStoreDbContext : DbContext
{
    public BookStoreDbContext()
    {
    }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrdersDetail> OrdersDetails { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Authors__70DAFC347701D1E1");

            entity.HasIndex(e => e.CompanyName, "IX_Authors_Company")
                .IsUnique()
                .HasFilter("([CompanyName] IS NOT NULL)");

            entity.HasIndex(e => new { e.FirstName, e.LastName }, "IX_Authors_First_Last")
                .IsUnique()
                .HasFilter("([CompanyName] IS NULL)");

            entity.Property(e => e.CompanyName).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Isbn).HasName("PK__Books__9271CED19A4C9817");

            entity.Property(e => e.Isbn).HasMaxLength(20);
            entity.Property(e => e.Price).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.Author)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Books__Author__489AC854");

            entity.HasOne(d => d.PublisherNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.Publisher)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Books__Publisher__498EEC8D");

            entity.HasMany(d => d.Discounts).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BooksDiscount",
                    r => r.HasOne<Discount>().WithMany()
                        .HasForeignKey("DiscountId")
                        .HasConstraintName("FK__BooksDisc__Disco__5F7E2DAC"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK__BooksDisc__BookI__5E8A0973"),
                    j =>
                    {
                        j.HasKey("BookId", "DiscountId").HasName("PK__BooksDis__43A334DE0CDCB125");
                        j.ToTable("BooksDiscounts");
                        j.IndexerProperty<string>("BookId").HasMaxLength(20);
                    });

            entity.HasMany(d => d.Genres).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BooksGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .HasConstraintName("FK__BooksGenr__Genre__4D5F7D71"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK__BooksGenr__BookI__4C6B5938"),
                    j =>
                    {
                        j.HasKey("BookId", "GenreId").HasName("PK__BooksGen__CDD89250C9B6C862");
                        j.ToTable("BooksGenres");
                        j.IndexerProperty<string>("BookId").HasMaxLength(20);
                    });
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8DFD2B53D");

            entity.HasIndex(e => e.Login, "UQ__Customer__5E55825B19097885").IsUnique();

            entity.Property(e => e.BuildingNo).HasMaxLength(5);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FlatNo).HasMaxLength(5);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(512);
            entity.Property(e => e.PhoneNumber).HasMaxLength(9);
            entity.Property(e => e.PostalCode).HasMaxLength(6);
            entity.Property(e => e.Street).HasMaxLength(100);

            entity.HasMany(d => d.Discounts).WithMany(p => p.Customers)
                .UsingEntity<Dictionary<string, object>>(
                    "CustomersDiscount",
                    r => r.HasOne<Discount>().WithMany()
                        .HasForeignKey("DiscountId")
                        .HasConstraintName("FK__Customers__Disco__5BAD9CC8"),
                    l => l.HasOne<Customer>().WithMany()
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK__Customers__Custo__5AB9788F"),
                    j =>
                    {
                        j.HasKey("CustomerId", "DiscountId").HasName("PK__Customer__DAED920102210C57");
                        j.ToTable("CustomersDiscounts");
                    });
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__E43F6D9661C7A894");

            entity.Property(e => e.DiscountName).HasMaxLength(100);
            entity.Property(e => e.Value).HasColumnType("decimal(3, 2)");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__0385057E1526D7B7");

            entity.HasIndex(e => e.GenreName, "UQ__Genres__BBE1C339E5FF426F").IsUnique();

            entity.Property(e => e.GenreName).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCFA4A0C7BE");

            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.State)
                .HasMaxLength(20)
                .HasDefaultValue("PENDING");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Orders__Customer__625A9A57");

            entity.HasOne(d => d.Discount).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DiscountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Orders__Discount__65370702");

            entity.HasOne(d => d.ShipperNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Shipper)
                .HasConstraintName("FK__Orders__Shipper__662B2B3B");
        });

        modelBuilder.Entity<OrdersDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.BookId }).HasName("PK__OrdersDe__A04E57EFBF63859C");

            entity.Property(e => e.BookId).HasMaxLength(20);

            entity.HasOne(d => d.Book).WithMany(p => p.OrdersDetails)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__OrdersDet__BookI__6BE40491");

            entity.HasOne(d => d.Order).WithMany(p => p.OrdersDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrdersDet__Order__6AEFE058");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("PK__Publishe__4C657FAB7ED8E01C");

            entity.HasIndex(e => e.PublisherName, "UQ__Publishe__5F0E22495E513168").IsUnique();

            entity.Property(e => e.PublisherName).HasMaxLength(100);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reviews__3214EC077E82EAB6");

            entity.Property(e => e.BookId).HasMaxLength(20);
            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Review1).HasColumnName("Review");

            entity.HasOne(d => d.Book).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Reviews__BookId__6FB49575");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Reviews__Custome__70A8B9AE");
        });

        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.HasKey(e => e.ShipperId).HasName("PK__Shippers__1F8AFE590CB169CE");

            entity.Property(e => e.PhoneNumber).HasMaxLength(9);
            entity.Property(e => e.ShipperName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
