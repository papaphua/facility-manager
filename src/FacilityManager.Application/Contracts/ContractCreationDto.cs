namespace FacilityManager.Application.Contracts;

public sealed record ContractCreationDto(
    Guid FacilityCode,
    Guid EquipmentCode,
    int Amount);