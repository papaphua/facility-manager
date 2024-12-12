using FacilityManager.Domain.Core;

namespace FacilityManager.Domain.Contracts;

public interface IContractRepository : IRepository<Contract>
{
    Task<Contract?> GetByCodesAsync(Guid facilityCode, Guid equipmentCode);

    public Task<List<Contract>> GetAllByFacilityCodeAsync(Guid facilityCode,
        bool includeEquipment = false);
}