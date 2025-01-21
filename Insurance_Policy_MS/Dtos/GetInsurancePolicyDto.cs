using Insurance_Policy_MS.Models;

namespace Insurance_Policy_MS.Dtos
{
    public class GetInsurancePolicyDto
    {
        public string PolicyNumber { get; set; }

        public string PolicyHolderName { get; set; }

        public string PolicyHolderAddress { get; set; }

        public string PolicyHolderEmail { get; set; }

        public string PolicyHolderPhone { get; set; }

        public string PolicyType { get; set; }

        public decimal CoverageAmount { get; set; }

        public decimal Premium { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
