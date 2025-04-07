using System.ComponentModel.DataAnnotations;

namespace CW2.Models
{
    public class CustomerViewModel
    {
        public long? CustomerId { get; set; } = null;

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime? BirthDate { get; set; }

        [MaxFileSize(10)] // Limit profile picture to 10 MB
        public byte[]? ProfilePic { get; set; }

        public required string Login { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$",
            ErrorMessage = "Password must include at least one uppercase letter, one lowercase letter, and one number.")]
        public string? PasswordHash { get; set; }

        [DataType(DataType.Password)]
        [Compare("PasswordHash", ErrorMessage = "Passwords do not match.")]
        [RequiredIfPasswordNotEmpty]
        public string? ConfirmPassword { get; set; }

        public string? PostalCode { get; set; }
        public string? Street { get; set; }

        [StringLength(5, ErrorMessage = "Building number cannot be longer than 5 characters")]
        public string? BuildingNo { get; set; }

        [StringLength(5, ErrorMessage = "Flat number cannot be longer than 5 characters")]
        public string? FlatNo { get; set; }

        public string? City { get; set; }

        [StringLength(12, ErrorMessage = "Phone number length cannot be more than 12")]
        [RegularExpression(@"^\d{2}-\d{3}-\d{2}-\d{2}$", ErrorMessage = "Phone number must be in the format 99-123-45-67.")]
        public required string PhoneNumber { get; set; }

        public bool AcceptsMarketing { get; set; } = false;
    }

    public class RequiredIfPasswordNotEmpty : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (CustomerViewModel)validationContext.ObjectInstance;

            bool isCreating = model.CustomerId == null;
            bool passwordSet = !string.IsNullOrWhiteSpace(model.PasswordHash);
            bool confirmSet = !string.IsNullOrWhiteSpace(model.ConfirmPassword);

            if (isCreating && !passwordSet)
            {
                return new ValidationResult("Password is required when creating a new user.");
            }

            if (passwordSet && !confirmSet)
            {
                return new ValidationResult("Confirm password is required when changing password.");
            }

            return ValidationResult.Success;
        }
    }

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSizeInBytes;

        public MaxFileSizeAttribute(int maxFileSizeInMB)
        {
            _maxFileSizeInBytes = maxFileSizeInMB * 1024 * 1024;
            ErrorMessage = $"File size must be less than or equal to {maxFileSizeInMB} MB.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var fileBytes = value as byte[];

            if (fileBytes != null && fileBytes.Length > _maxFileSizeInBytes)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
