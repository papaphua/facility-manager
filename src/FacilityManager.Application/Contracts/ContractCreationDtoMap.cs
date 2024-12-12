using AutoMapper;
using FacilityManager.Domain.Contracts;

namespace FacilityManager.Application.Contracts;

public sealed class ContractCreationDtoMap : Profile
{
    public ContractCreationDtoMap()
    {
        CreateMap<ContractCreationDto, Contract>();
    }
}