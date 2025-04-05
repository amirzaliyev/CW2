namespace CW2.DAL.Entities
{
    public class Order
    {
        public enum Status
        {
            Pending,
            Paid,
            Sent
        }

        public long? OrderId;
        public required int CustomerId;
        public DateTime? OrderDate;
        public int? DiscountId;
        public required int ShipperId;
        public decimal? TotalAmount;
        public Status? State;
    }
}
