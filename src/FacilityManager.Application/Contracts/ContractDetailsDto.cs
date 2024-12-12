namespace FacilityManager.Application.Contracts;

public sealed record ContractDetailsDto(
    string FacilityName,
    string EquipmentName,
    int Amount);