using FacilityManager.Domain.Core;

namespace FacilityManager.Domain.Facilities;

public interface IFacilityRepository : IRepository<Facility>
{
    Task<Facility?> GetAsync(Guid code);
}