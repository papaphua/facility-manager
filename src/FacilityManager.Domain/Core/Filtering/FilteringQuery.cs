namespace FacilityManager.Domain.Core.Filtering;

public sealed record FilteringQuery(
    string? Attribute = null,
    string Order = "asc");