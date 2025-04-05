namespace CW2.DAL.Entities
{
    public class Review
    {
        public int? ReviewId;
        public required string BookId;
        public required int CustomerId;
        public required int BookReview;
        public DateTime? ReviewDate;
    }
}
