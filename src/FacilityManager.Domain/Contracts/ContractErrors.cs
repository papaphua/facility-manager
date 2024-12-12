using FacilityManager.Domain.Core.Results;

namespace FacilityManager.Domain.Contracts;

public static class ContractErrors
{
    public static readonly Error AmountError = Error.Internal(
        "Contract.AmountError", "The contract amount must be greater than 0.");

    public static readonly Error CreateError = Error.Internal(
        "Contract.CreateError", "An error occurred while creating a new contract.");
}