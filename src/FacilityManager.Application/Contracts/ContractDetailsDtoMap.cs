using AutoMapper;
using FacilityManager.Domain.Contracts;

namespace FacilityManager.Application.Contracts;

public sealed class ContractDetailsDtoMap : Profile
{
    public ContractDetailsDtoMap()
    {
        CreateMap<Contract, ContractDetailsDto>()
            .ForMember(dest => dest.FacilityName, opt =>
                opt.MapFrom(src => src.Facility.Name))
            .ForMember(dest => dest.EquipmentName, opt =>
                opt.MapFrom(src => src.Equipment.Name))
            .ForMember(dest => dest.Amount, opt =>
                opt.MapFrom(src => src.Amount));
    }
}