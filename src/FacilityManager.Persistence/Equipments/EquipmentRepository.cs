using FacilityManager.Domain.Equipments;
using FacilityManager.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace FacilityManager.Persistence.Equipments;

public sealed class EquipmentRepository(ApplicationDbContext db)
    : Repository<Equipment>(db), IEquipmentRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task<Equipment?> GetAsync(Guid code)
    {
        return await _db.Set<Equipment>()
            .FirstOrDefaultAsync(equipment => equipment.Code == code);
    }
}