using Insurance_Policy_MS.Models;
using System.ComponentModel.DataAnnotations;

namespace Insurance_Policy_MS.Dtos
{
    public class CreateInsuranceDto
    {
        [Required(ErrorMessage = "Policy number is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Policy number must be between 3 and 50 characters")]
        public string PolicyNumber { get; set; }

        [Required(ErrorMessage = "Policy holder name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Policy holder name must be between 2 and 100 characters")]
        public string PolicyHolderName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string PolicyHolderAddress { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100)]
        public string PolicyHolderEmail { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(20)]
        public string PolicyHolderPhone { get; set; }

        [Required(ErrorMessage = "Policy type is required")]
        public PolicyType PolicyType { get; set; }

        [Required(ErrorMessage = "Coverage amount is required")]
        [Range(1000, 10000000, ErrorMessage = "Coverage amount must be between 1,000 and 10,000,000")]
        public decimal CoverageAmount { get; set; }

        [Required(ErrorMessage = "Premium is required")]
        [Range(100, 1000000, ErrorMessage = "Premium must be between 100 and 1,000,000")]
        public decimal Premium { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        // Custom validation method for date range
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate <= StartDate)
            {
                yield return new ValidationResult(
                    "End date must be after start date",
                    new[] { nameof(EndDate) });
            }

            if (StartDate < DateTime.UtcNow.Date)
            {
                yield return new ValidationResult(
                    "Start date cannot be in the past",
                    new[] { nameof(StartDate) });
            }
        }
    }

}
