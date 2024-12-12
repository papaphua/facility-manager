namespace FacilityManager.Application.Contracts;

public sealed record ContractDto(
    Guid FacilityCode,
    Guid EquipmentCode,
    int Amount);