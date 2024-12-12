using FacilityManager.Domain.Core;
using FacilityManager.Domain.Core.Paging;

namespace FacilityManager.Domain.Contracts;

public interface IContractRepository : IRepository<Contract>
{
    public Task<List<Contract>> GetAllByFacilityCodeAsync(Guid facilityCode,
        bool includeEquipment = false);

    public Task<PagedList<Contract>> GetAllAsync(PagingQuery? paging);
}