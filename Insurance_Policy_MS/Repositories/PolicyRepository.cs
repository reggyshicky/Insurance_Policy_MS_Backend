using AutoMapper;
using Insurance_Policy_MS.Data;
using Insurance_Policy_MS.Dtos;
using Insurance_Policy_MS.Models;
using Insurance_Policy_MS.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Insurance_Policy_MS.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly InsuranceDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public PolicyRepository(InsuranceDbContext context, IMapper mapper, ILogger<PolicyRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<GetInsurancePolicyDto>> CreateAsync(CreateInsuranceDto dto)
        {
            try
            {
                var policy = _mapper.Map<InsurancePolicy>(dto);

                var checKIfExists = await _context.Policies.FirstOrDefaultAsync(x => x.PolicyNumber == dto.PolicyNumber);
                if (checKIfExists is not null)
                {
                    return new Response<GetInsurancePolicyDto>
                    {
                        Message = "Policy already exists",
                        Data = null,
                        Status = HttpStatusCode.BadRequest

                    };
                }


                if (policy.PolicyNumber == dto.PolicyNumber)
                {

                }
                policy.Status = PolicyStatus.Active.ToString();
                await _context.Policies.AddAsync(policy);
                await _context.SaveChangesAsync();
                return new Response<GetInsurancePolicyDto>
                {
                    Message = "Insurance Policy Created Succesfully!",
                    Status = HttpStatusCode.Created,
                    Data = _mapper.Map<GetInsurancePolicyDto>(policy)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"error, {ex}");
                return new Response<GetInsurancePolicyDto>
                {
                    Message = "Error occured while creating an insurance policy",
                    Status = HttpStatusCode.InternalServerError,
                    Data = null
                };
            }
        }

        public async Task<List<GetInsurancePolicyDto>> GetAllAsync()
        {
            var policies = await _context.Policies
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            List<GetInsurancePolicyDto> dtoPolicies = new List<GetInsurancePolicyDto>();
            for (int i = 0; i <= policies.Count; i++)
            {
                var x = _mapper.Map<GetInsurancePolicyDto>(policies[i]);
                dtoPolicies.Add(x);
            }

            return new Response<List<GetInsurancePolicyDto>>
            {
                Message = "Policis retrieved succesfully!",
                Data = dtoPolicies,
                Status = HttpStatusCode.OK

            };
        }

        public async Task<InsurancePolicy> GetByIdAsync(Guid id)
        {
            return await _context.Policies.FindAsync(id);
        }



        public async Task<InsurancePolicy> UpdateAsync(Guid id, InsurancePolicy insurancePolicy)
        {
            InsurancePolicy existingPolicy = await _context.Policies.FindAsync(id);
            if (existingPolicy == null)
                return null;

            existingPolicy.PolicyHolderName = existingPolicy.PolicyHolderName;
            existingPolicy.PolicyHolderAddress = existingPolicy.PolicyHolderAddress;
            existingPolicy.PolicyHolderEmail = existingPolicy.PolicyHolderEmail;
            existingPolicy.PolicyHolderPhone = existingPolicy.PolicyHolderPhone;
            existingPolicy.CoverageAmount = existingPolicy.CoverageAmount;
            existingPolicy.Premium = existingPolicy.Premium;
            existingPolicy.StartDate = existingPolicy.StartDate;
            existingPolicy.EndDate = existingPolicy.EndDate;
            existingPolicy.Status = existingPolicy.Status;
            existingPolicy.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingPolicy;
        }

        public async Task<bool?> DeleteAsync(Guid id)
        {
            var policy = await _context.Policies.FindAsync(id);
            if (policy == null)
                return false;

            _context.Policies.Remove(policy);

            await _context.SaveChangesAsync();
            return true;

        }
    }
}
