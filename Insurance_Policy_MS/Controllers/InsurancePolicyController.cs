using AutoMapper;
using Insurance_Policy_MS.Dtos;
using Insurance_Policy_MS.Models;
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
        public async Task<ActionResult<Response<GetInsurancePolicyDto>>> CreateInsurancePolicy(CreateInsuranceDto dto)
        {
            return await _repo.CreateAsync(dto);
        }
    }
}
