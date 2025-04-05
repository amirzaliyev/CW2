using CW2.DAL.Entities;

namespace CW2.Models
{
    public class OrderDetailsViewModel
    {
        public required string BookId;
        public required int OrderId;
        public required int OrderQuantity;

        public required Book Book;
        public required Order Order;
    }
}
