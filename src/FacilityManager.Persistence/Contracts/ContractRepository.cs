using FacilityManager.Domain.Contracts;
using FacilityManager.Domain.Core.Paging;
using FacilityManager.Persistence.Core;
using FacilityManager.Persistence.Core.Paging;
using Microsoft.EntityFrameworkCore;

namespace FacilityManager.Persistence.Contracts;

public sealed class ContractRepository(ApplicationDbContext db)
    : Repository<Contract>(db), IContractRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task<Contract?> GetByCodesAsync(Guid facilityCode, Guid equipmentCode)
    {
        return await _db.Set<Contract>()
            .FirstOrDefaultAsync(contract => contract.FacilityCode == facilityCode &&
                                             contract.EquipmentCode == equipmentCode);
    }

    public async Task<List<Contract>> GetAllByFacilityCodeAsync(Guid facilityCode,
        bool includeEquipment = false)
    {
        var query = _db.Set<Contract>()
            .Where(contract => contract.FacilityCode == facilityCode);

        if (includeEquipment)
            query = query.Include(contract => contract.Equipment);

        return await query.ToListAsync();
    }

    public async Task<PagedList<Contract>> GetAllAsync(PagingQuery? paging)
    {
        var query = _db.Set<Contract>()
            .OrderBy(contract => contract.Facility.Name)
            .ThenBy(contract => contract.Equipment.Name)
            .Include(contract => contract.Facility)
            .Include(contract => contract.Equipment);

        // Return a paginated list if paging information is provided
        if (paging is { PageNumber: > 0, PageSize: > 0 }) return await query.ToPagedListAsync(paging);

        // Return a full list if paging is not provided
        // This assumes there is only one page containing all the contracts
        return new PagedList<Contract>(await query.ToListAsync());
    }
}