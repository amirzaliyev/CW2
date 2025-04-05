namespace CW2.Models
{
    public class ReviewViewOrder
    {
        public int? ReviewId;
        public required string BookId;
        public required int CustomerId;
        public required int Review;
        public DateTime? ReviewDate;
    }
}
