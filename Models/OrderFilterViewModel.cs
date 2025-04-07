using System.ComponentModel.DataAnnotations;

namespace CW2.Models
{
    public class OrderFilterViewModel
    {
        public required string CustomerName { get; set; }

        [DataType(DataType.Date)]
        public required DateTime OrderDateFrom { get; set; } = new DateTime(1901, 1, 1);
        public string? State { get; set; }
        public string? ShipperName { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortColumn { get; set; } = "OrderId";
        public bool SortDesc { get; set; } = false;
        public int? TotalCount { get; set; }
        public IEnumerable<OrderJoinViewModel> Orders { get; set; }
    }
}
