using AutoMapper;
using FacilityManager.Application.Core;
using FacilityManager.Domain.Contracts;
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
    public async Task<Result> CreateAsync(ContractDto dto)
    {
        var facility = await facilityRepository.GetAsync(dto.FacilityCode);

        if (facility is null)
            return FacilityErrors.NotFound;

        var equipment = await equipmentRepository.GetAsync(dto.EquipmentCode);

        if (equipment is null)
            return EquipmentErrors.NotFound;

        var existingContract = await contractRepository.GetByCodesAsync(dto.FacilityCode, dto.EquipmentCode);

        if (existingContract is not null)
            return ContractErrors.AlreadyExists;

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
}