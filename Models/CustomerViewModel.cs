using System.ComponentModel.DataAnnotations;

namespace CW2.Models
{
    public class CustomerViewModel
    {
        public long? CustomerId { get; set; } = null;
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public byte[]? ProfilePic { get; set; }
        public required string Login { get; set; }

        // Password validation
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$",
            ErrorMessage = "Password must include at least one uppercase letter, one lowercase letter, and one number.")]
        public string? PasswordHash { get; set; } 
        [DataType(DataType.Password)]
        [Compare("PasswordHash", ErrorMessage = "Passwords do not match.")]
        [RequiredIfPasswordNotEmpty]
        public string? ConfirmPassword { get; set; } 

        public required string PostalCode { get; set; }
        public required string Street { get; set; }
        public required string BuildingNo { get; set; }
        public string? FlatNo { get; set; }
        public required string City { get; set; }
        public string? Tin { get; set; }  // Tax identification number
        public required string PhoneNumber { get; set; }
    }

    
    public class RequiredIfPasswordNotEmpty : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (CustomerViewModel)validationContext.ObjectInstance;
            if (!string.IsNullOrEmpty(model.PasswordHash) && string.IsNullOrEmpty(model.ConfirmPassword))
            {
                return new ValidationResult("Confirm password is required when changing password.");
            }
            return ValidationResult.Success;
        }
    }
}
