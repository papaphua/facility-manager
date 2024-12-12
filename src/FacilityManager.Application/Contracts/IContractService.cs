using FacilityManager.Domain.Core.Results;

namespace FacilityManager.Application.Contracts;

public interface IContractService
{
    Task<Result> CreateAsync(ContractDto dto);
}