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

        public async Task<Response<InsurancePolicyDto>> CreateAsync(CreateInsuranceDto dto)
        {
            try
            {
                var policy = _mapper.Map<InsurancePolicy>(dto);

                var checKIfExists = await _context.Policies.FirstOrDefaultAsync(x => x.PolicyNumber == dto.PolicyNumber);
                if (checKIfExists is not null)
                {
                    return new Response<InsurancePolicyDto>
                    {
                        Message = "Policy already exists",
                        Data = null,
                        Status = HttpStatusCode.BadRequest

                    };
                }

                policy.Status = PolicyStatus.Active.ToString();
                await _context.Policies.AddAsync(policy);
                await _context.SaveChangesAsync();
                return new Response<InsurancePolicyDto>
                {
                    Message = "Insurance Policy Created Succesfully!",
                    Status = HttpStatusCode.Created,
                    Data = _mapper.Map<InsurancePolicyDto>(policy)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"error, {ex}");
                return new Response<InsurancePolicyDto>
                {
                    Message = "Error occured while creating an insurance policy",
                    Status = HttpStatusCode.InternalServerError,
                    Data = null
                };
            }
        }

        public async Task<Response<List<InsurancePolicyDto>>> GetAllAsync()
        {
            var policies = await _context.Policies
                .Where(x => x.Status != "Deleted")
                .ToListAsync();

            List<InsurancePolicyDto> dtoPolicies = new List<InsurancePolicyDto>();
            for (int i = 0; i < policies.Count; i++)
            {
                var x = _mapper.Map<InsurancePolicyDto>(policies[i]);
                dtoPolicies.Add(x);
            }

            return new Response<List<InsurancePolicyDto>>
            {
                Message = "Policis retrieved succesfully!",
                Data = dtoPolicies,
                Status = HttpStatusCode.OK
            };
        }

        public async Task<Response<InsurancePolicyDto?>> GetPolicyAsync(string policyNumber)
        {
            var policy = await _context.Policies.FirstOrDefaultAsync(x => x.PolicyNumber == policyNumber && x.Status != "Deleted");
            if (policy is null)
            {
                return new Response<InsurancePolicyDto?>
                {
                    Message = "Insurance Policy does not exist!!",
                    Data = null,
                    Status = HttpStatusCode.OK
                };
            }
            return new Response<InsurancePolicyDto?>
            {
                Message = "Insurance Policy retrieved succesfully!!",
                Data = _mapper.Map<InsurancePolicyDto>(policy),
                Status = HttpStatusCode.OK
            };
        }



        public async Task<Response<InsurancePolicyDto>> UpdateAsync(string policyNumber, InsurancePolicyDto insurancePolicy)
        {
            try
            {
                var existingPolicy = await _context.Policies.FirstOrDefaultAsync(x => x.PolicyNumber == policyNumber);
                if (existingPolicy == null)
                {
                    return new Response<InsurancePolicyDto>
                    {
                        Message = "Insurance Policy to be updated does not exist",
                        Data = null,
                        Status = HttpStatusCode.BadRequest
                    };
                }

                existingPolicy.PolicyHolderName = insurancePolicy.PolicyHolderName;
                existingPolicy.PolicyHolderAddress = insurancePolicy.PolicyHolderAddress;
                existingPolicy.PolicyHolderEmail = insurancePolicy.PolicyHolderEmail;
                existingPolicy.PolicyHolderPhone = insurancePolicy.PolicyHolderPhone;
                existingPolicy.CoverageAmount = insurancePolicy.CoverageAmount;
                existingPolicy.Premium = insurancePolicy.Premium;
                existingPolicy.StartDate = insurancePolicy.StartDate;
                existingPolicy.EndDate = insurancePolicy.EndDate;
                existingPolicy.Status = insurancePolicy.Status;
                existingPolicy.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return new Response<InsurancePolicyDto>
                {
                    Message = "Insurance Policy updated succesfully!",
                    Data = _mapper.Map<InsurancePolicyDto>(existingPolicy),
                    Status = HttpStatusCode.OK
                };
            }
            catch (Exception error)
            {
                Console.WriteLine("Error" + error);
                return new Response<InsurancePolicyDto>
                {
                    Message = "Error occurred while updating the policy",
                    Data = null,
                    Status = HttpStatusCode.InternalServerError
                };

            }

        }

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
            policy.Status = PolicyStatus.Deleted.ToString();
            //_context.Policies.Remove(policy); //we use soft delete

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
