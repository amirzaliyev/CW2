namespace CW2.Models
{
    public class CustomerFilterViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PostalCode { get; set; }
        public string? Street { get; set; }
        public string? BuildingNo { get; set; }
        public string? FlatNo { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortColumn { get; set; } = "CustomerId";
        public bool SortDesc { get; set; } = false;
        public int? TotalCount { get; set; }
        public IEnumerable<CustomerViewModel> Customers { get; set; }

    }
}
