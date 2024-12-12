using FacilityManager.Domain.Facilities;
using FacilityManager.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace FacilityManager.Persistence.Facilities;

public sealed class FacilityRepository(ApplicationDbContext db)
    : Repository<Facility>(db), IFacilityRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task<Facility?> GetAsync(Guid code)
    {
        return await _db.Set<Facility>()
            .FirstOrDefaultAsync(facility => facility.Code == code);
    }
}