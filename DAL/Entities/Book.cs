namespace CW2.DAL.Entities
{
    public class Book
    {
        public required string Isbn;
        public required string Title;
        public DateTime? PublicationDate;
        public int? edition;
        public int? AvailableQuantity;
        public required decimal Price;
        public int? AuthorId;
        public int? PublisherId;
        public byte[]? Thumbnail;
        public string? ShortDescription;
    }
}
