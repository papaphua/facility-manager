using FacilityManager.Domain.Core;
using FacilityManager.Domain.Equipments;
using FacilityManager.Domain.Facilities;

namespace FacilityManager.Domain.Contracts;

public sealed class Contract : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid FacilityCode { get; set; }

    public Guid EquipmentCode { get; set; }

    public int Amount { get; set; }

    public Facility Facility { get; set; }

    public Equipment Equipment { get; set; }
}