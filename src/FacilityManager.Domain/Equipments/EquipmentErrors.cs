using FacilityManager.Domain.Core.Results;

namespace FacilityManager.Domain.Equipments;

public static class EquipmentErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Equipment.NotFound", "The specified equipment could not be found.");
}