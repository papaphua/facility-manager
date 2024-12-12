using AutoMapper;
using FacilityManager.Domain.Contracts;

namespace FacilityManager.Application.Contracts;

public sealed class ContractMap : Profile
{
    public ContractMap()
    {
        CreateMap<Contract, ContractDto>();
        CreateMap<ContractDto, Contract>();
    }
}