using FacilityManager.Domain.Core.Results;

namespace FacilityManager.Domain.Facilities;

public static class FacilityErrors
{
    public static readonly Error NotEnoughArea = Error.Validation(
        "Facility.NotEnoughArea", "Facility does not have enough area for this equipment.");

    public static readonly Error NotFound = Error.NotFound(
        "Facility.NotFound", "The specified facility could not be found.");
}