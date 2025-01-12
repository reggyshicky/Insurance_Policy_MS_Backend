using Insurance_Policy_MS.Models;

namespace Insurance_Policy_MS.Repositories
{
    public interface IPolicyRepository
    {
        Task<List<InsurancePolicy>> GetAllAsync();
        Task<InsurancePolicy?> GetByIdAsync(Guid Id);
        Task<InsurancePolicy> CreateAsync(InsurancePolicy insurancePolicy);
        Task<InsurancePolicy> UpdateAsync(Guid id, InsurancePolicy insurancePolicy);

        Task<bool?> DeleteAsync(Guid id);

    }
}
