using Insurance_Policy_MS.Dtos;
using Insurance_Policy_MS.Models;

namespace Insurance_Policy_MS.Repositories
{
    public interface IPolicyRepository
    {
        Task<List<GetInsurancePolicyDto>> GetAllAsync();
        Task<InsurancePolicy?> GetByIdAsync(Guid Id);
        Task<Response<GetInsurancePolicyDto>> CreateAsync(CreateInsuranceDto dto);
        Task<InsurancePolicy> UpdateAsync(Guid id, InsurancePolicy insurancePolicy);

        Task<bool?> DeleteAsync(Guid id);

    }
}
