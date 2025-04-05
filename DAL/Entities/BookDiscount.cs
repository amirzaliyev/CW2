namespace CW2.DAL.Entities
{
    public class BookDiscount
    {
        public required string BookId;
        public required int DiscountId;

        public required Book Book;
        public required Discount Discount;
    }
}
