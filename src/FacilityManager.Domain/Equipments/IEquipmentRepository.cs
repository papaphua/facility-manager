using FacilityManager.Domain.Core;

namespace FacilityManager.Domain.Equipments;

public interface IEquipmentRepository : IRepository<Equipment>
{
    Task<Equipment?> GetAsync(Guid code);
}