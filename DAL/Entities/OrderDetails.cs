namespace CW2.DAL.Entities
{
    public class OrderDetails
    {
        public required string BookId;
        public required int OrderId;
        public required int OrderQuantity;

        public required Book Book;
        public required Order Order;
    }
}
