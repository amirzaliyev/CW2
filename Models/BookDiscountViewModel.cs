using CW2.DAL.Entities;

namespace CW2.Models
{
    public class BookDiscountViewModel
    {
        public required string BookId;
        public required int DiscountId;

        public required Book Book;
        public required Discount Discount;
    }
}
