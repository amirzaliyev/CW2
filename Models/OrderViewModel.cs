using System;
using System.ComponentModel.DataAnnotations;

namespace CW2.Models
{
    public class OrderViewModel
    {
        public long? OrderId { get; set; }

        public required long CustomerId { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date is required.")]
        public required DateTime Date { get; set; }

        public required long? DiscountId { get; set; }

        
        public required long Shipper { get; set; }

        [RegularExpression("PENDING|PAID|SENT", ErrorMessage = "State must be PENDING, PAID, or SENT.")]
        public required string State { get; set; } = "PENDING";
    }
}