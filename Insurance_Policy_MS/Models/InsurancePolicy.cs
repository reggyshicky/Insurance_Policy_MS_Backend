using System.ComponentModel.DataAnnotations;

namespace Insurance_Policy_MS.Models
{
    public class InsurancePolicy
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string PolicyNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string PolicyHolderName { get; set; }

        [Required]
        [MaxLength(200)]
        public string PolicyHolderAddress { get; set; }

        [Required]
        [EmailAddress]
        public string PolicyHolderEmail { get; set; }

        [Required]
        [Phone]
        public string PolicyHolderPhone { get; set; }

        [Required]
        public PolicyType PolicyType { get; set; }

        [Required]
        public decimal CoverageAmount { get; set; }

        [Required]
        public decimal Premium { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public PolicyStatus Status { get; set; } = PolicyStatus.Active;


    }

    public enum PolicyStatus
    {
        Active,
        Expired,
        Cancelled,
        Pending
    }

    public enum PolicyType
    {
        Life,
        Health,
        Auto,
        Property,
        Travel,
        Business
    }
}
