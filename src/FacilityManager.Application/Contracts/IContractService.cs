using FacilityManager.Domain.Core.Paging;
using FacilityManager.Domain.Core.Results;

namespace FacilityManager.Application.Contracts;

public interface IContractService
{
    Task<Result> CreateAsync(ContractCreationDto dto);

    Task<Result<PagedList<ContractDetailsDto>>> GetAllAsync(PagingQuery? paging);
}