using Insurance_Policy_MS.Data;
using Insurance_Policy_MS.Dtos;
using Insurance_Policy_MS.Models;
using Microsoft.EntityFrameworkCore;

namespace Insurance_Policy_MS.Repositories
{
    public class PolicyRepository
    {
        private readonly InsuranceDbContext _context;
        public PolicyRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task<List<InsurancePolicy>> GetAllAsync()
        {
            return await _context.Policies
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<InsurancePolicy> GetByIdAsync(Guid id)
        {
            return await _context.Policies.FindAsync(id);
        }

        public async Task<InsurancePolicy> CreateAsync(InsurancePolicy policy)
        {
            await _context.Policies.AddAsync(policy);
            await _context.SaveChangesAsync();
            return policy;
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

        public async Task<bool> DeleteAsync(Guid id)
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
