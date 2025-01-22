using AutoMapper;
using Insurance_Policy_MS.Dtos;
using Insurance_Policy_MS.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_Policy_MS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class InsurancePolicyController : ControllerBase
    {
        private readonly IPolicyRepository _repo;
        public InsurancePolicyController(IPolicyRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Response<InsurancePolicyDto>>> CreateInsurancePolicy([FromBody] CreateInsuranceDto dto)
        {
            return await _repo.CreateAsync(dto);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult<Response<List<InsurancePolicyDto>>>> GetInsurancePolicies()
        {
            return await _repo.GetAllAsync();
        }

        [HttpGet]
        [Route("getpolicy/{policyNumber}")]
        public async Task<ActionResult<Response<InsurancePolicyDto?>>> GetInsurancePolicy(string policyNumber)
        {
            return await _repo.GetPolicyAsync(policyNumber);
        }

        [HttpDelete]
        [Route("deletepolicy/{policyNumber}")]
        public async Task<ActionResult<Response<bool?>>> DeletePolicy(string policyNumber)
        {
            return await _repo.DeleteAsync(policyNumber);
        }
    }
}
