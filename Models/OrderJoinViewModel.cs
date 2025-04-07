namespace CW2.Models
{
    public class OrderJoinViewModel
    {
        public long OrderId { get; set; }
        public DateTime Date { get; set; }
        public string State { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string ShipperName { get; set; } = null!;
    }
}
