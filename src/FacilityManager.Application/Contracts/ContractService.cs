using AutoMapper;
using FacilityManager.Application.Core;
using FacilityManager.Domain.Contracts;
using FacilityManager.Domain.Core.Paging;
using FacilityManager.Domain.Core.Results;
using FacilityManager.Domain.Equipments;
using FacilityManager.Domain.Facilities;

namespace FacilityManager.Application.Contracts;

public sealed class ContractService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IFacilityRepository facilityRepository,
    IEquipmentRepository equipmentRepository,
    IContractRepository contractRepository)
    : IContractService
{
    public async Task<Result> CreateAsync(ContractCreationDto dto)
    {
        if (dto.Amount <= 0)
            return ContractErrors.AmountError;

        var facility = await facilityRepository.GetAsync(dto.FacilityCode);

        if (facility is null)
            return FacilityErrors.NotFound;

        var equipment = await equipmentRepository.GetAsync(dto.EquipmentCode);

        if (equipment is null)
            return EquipmentErrors.NotFound;

        var contracts = await contractRepository
            .GetAllByFacilityCodeAsync(dto.FacilityCode, true);
        var usedArea = contracts.Aggregate(0f, (current, contract) =>
            current + contract.Amount * contract.Equipment.Area);

        var resultArea = usedArea + dto.Amount * equipment.Area;

        if (resultArea > facility.StandardArea)
            return FacilityErrors.NotEnoughArea;

        var contract = mapper.Map<Contract>(dto);

        try
        {
            await contractRepository.AddAsync(contract);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception)
        {
            return ContractErrors.CreateError;
        }

        return Result.Success();
    }

    public async Task<Result<PagedList<ContractDetailsDto>>> GetAllAsync(PagingQuery? paging)
    {
        var contracts = await contractRepository.GetAllAsync(paging);
        var dtos = contracts.Select(mapper.Map<ContractDetailsDto>);

        // Required to retrieve back paging information.
        // If PagingQuery was not provided, it will still contain info indicating that there is only one page.
        return dtos.AsPagedList(contracts.Info);
    }
}