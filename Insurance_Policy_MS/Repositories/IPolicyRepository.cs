using Insurance_Policy_MS.Dtos;
using Insurance_Policy_MS.Models;

namespace Insurance_Policy_MS.Repositories
{
    public interface IPolicyRepository
    {
        Task<Response<List<InsurancePolicyDto>>> GetAllAsync();
        Task<Response<InsurancePolicyDto?>> GetPolicyAsync(string policyNumber);
        Task<Response<InsurancePolicyDto>> CreateAsync(CreateInsuranceDto dto);
        Task<Response<InsurancePolicyDto>> UpdateAsync(string policyNumber, InsurancePolicyDto insurancePolicy);

        Task<Response<bool?>> DeleteAsync(string policyNumber);

    }
}
