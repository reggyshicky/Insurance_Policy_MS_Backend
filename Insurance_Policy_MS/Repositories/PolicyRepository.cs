using AutoMapper;
using Insurance_Policy_MS.Data;
using Insurance_Policy_MS.Dtos;
using Insurance_Policy_MS.Models;
using Insurance_Policy_MS.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public async Task<Response<List<GetInsurancePolicyDto>>> GetAllAsync()
        {
            var policies = await _context.Policies
                .ToListAsync();

            List<GetInsurancePolicyDto> dtoPolicies = new List<GetInsurancePolicyDto>();
            for (int i = 0; i < policies.Count; i++)
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

        public async Task<Response<GetInsurancePolicyDto?>> GetPolicyAsync(string policyNumber)
        {
            var policy = await _context.Policies.FirstOrDefaultAsync(x => x.PolicyNumber == policyNumber);
            if (policy is null)
            {
                return new Response<GetInsurancePolicyDto?>
                {
                    Message = "Insurance Policy does not exist!!",
                    Data = null,
                    Status = HttpStatusCode.OK
                };
            }
            return new Response<GetInsurancePolicyDto?>
            {
                Message = "Insurance Policy retrieved succesfully!!",
                Data = _mapper.Map<GetInsurancePolicyDto>(policy),
                Status = HttpStatusCode.OK
            };
        }



        //public async Task<Response<InsurancePolicy>> UpdateAsync(Guid id, InsurancePolicy insurancePolicy)
        //{
        //    InsurancePolicy existingPolicy = await _context.Policies.FindAsync(id);
        //    if (existingPolicy == null)
        //        return null;

        //    existingPolicy.PolicyHolderName = existingPolicy.PolicyHolderName;
        //    existingPolicy.PolicyHolderAddress = existingPolicy.PolicyHolderAddress;
        //    existingPolicy.PolicyHolderEmail = existingPolicy.PolicyHolderEmail;
        //    existingPolicy.PolicyHolderPhone = existingPolicy.PolicyHolderPhone;
        //    existingPolicy.CoverageAmount = existingPolicy.CoverageAmount;
        //    existingPolicy.Premium = existingPolicy.Premium;
        //    existingPolicy.StartDate = existingPolicy.StartDate;
        //    existingPolicy.EndDate = existingPolicy.EndDate;
        //    existingPolicy.Status = existingPolicy.Status;
        //    existingPolicy.UpdatedAt = DateTime.UtcNow;

        //    await _context.SaveChangesAsync();
        //    return existingPolicy;
        //}

        public async Task<Response<bool?>> DeleteAsync(string policyNumber)
        {
            var policy = await _context.Policies.FirstOrDefaultAsync(x => x.PolicyNumber == policyNumber);
            if (policy is null)
            {
                return new Response<bool?>
                {
                    Message = "Insurance Policy does not exist!!",
                    Data = false,
                    Status = HttpStatusCode.BadRequest
                };
            }


            _context.Policies.Remove(policy);

            await _context.SaveChangesAsync();
            return new Response<bool?>
            {
                Message = "Insurance Policy deleted succesfully",
                Data = true,
                Status = HttpStatusCode.OK
            };

        }
    }
}
