namespace FacilityManager.Domain.Core.Results;

public enum ErrorType
{
    NotFound = 404,
    Validation = 400,
    Conflict = 409,
    Internal = 500
}