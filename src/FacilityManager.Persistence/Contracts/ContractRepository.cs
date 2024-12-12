using FacilityManager.Domain.Contracts;
using FacilityManager.Persistence.Core;
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
}