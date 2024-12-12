using FacilityManager.Domain.Contracts;
using FacilityManager.Domain.Core;

namespace FacilityManager.Domain.Equipments;

public sealed class Equipment : IEntity
{
    public Guid Code { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public float Area { get; set; }

    public ICollection<Contract> Contracts { get; set; }
}