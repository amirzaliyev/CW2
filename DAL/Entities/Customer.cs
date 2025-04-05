using System.Numerics;

namespace CW2.DAL.Entities
{
    public class Customer
    {
        public long? CustomerId { get; set; } = null;
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public byte[]? ProfilePic { get; set; }
        public required string Login { get; set; }
        public required string PasswordHash { get; set; }
        public required string PostalCode { get; set; }
        public required string Street { get; set; }
        public required string BuildingNo { get; set; }
        public string? FlatNo { get; set; }
        public required string City { get; set; }
        public string? Tin { get; set; }  // Tax identification number
        public required string PhoneNumber { get; set; }
    }
}
