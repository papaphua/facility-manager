using FacilityManager.Domain.Core.Results;

namespace FacilityManager.Domain.Contracts;

public static class ContractErrors
{
    public static readonly Error AlreadyExists = Error.Conflict(
        "Contract.AlreadyExists", "A contract with the specified facility and equipment already exists.");

    public static readonly Error CreateError = Error.Internal(
        "Contract.CreateError", "An error occurred while creating a new contract.");
}