using Insurance_Policy_MS.Dtos;
using Insurance_Policy_MS.Models;

namespace Insurance_Policy_MS.Repositories
{
    public interface IPolicyRepository
    {
        Task<Response<List<GetInsurancePolicyDto>>> GetAllAsync();
        Task<Response<GetInsurancePolicyDto?>> GetPolicyAsync(string policyNumber);
        Task<Response<GetInsurancePolicyDto>> CreateAsync(CreateInsuranceDto dto);
        //Task<Response<InsurancePolicy>> UpdateAsync(Guid id, InsurancePolicy insurancePolicy);

        Task<Response<bool?>> DeleteAsync(string policyNumber);

    }
}
