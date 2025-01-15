using AutoMapper;
using Insurance_Policy_MS.Dtos;
using Insurance_Policy_MS.Models;

namespace Insurance_Policy_MS.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<InsurancePolicy, CreateInsuranceDto>().ReverseMap();
        }
    }
}
