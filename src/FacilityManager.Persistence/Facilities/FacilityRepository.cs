using FacilityManager.Domain.Facilities;
using FacilityManager.Persistence.Core;

namespace FacilityManager.Persistence.Facilities;

public sealed class FacilityRepository(ApplicationDbContext db)
    : Repository<Facility>(db), IFacilityRepository;