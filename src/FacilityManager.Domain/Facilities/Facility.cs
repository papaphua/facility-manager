using FacilityManager.Domain.Contracts;
using FacilityManager.Domain.Core;

namespace FacilityManager.Domain.Facilities;

public sealed class Facility : IEntity
{
    public Guid Code { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public float StandardArea { get; set; }

    public ICollection<Contract> Contracts { get; set; }
}