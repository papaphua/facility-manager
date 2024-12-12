using AutoMapper;
using FacilityManager.Application.Contracts;
using FacilityManager.Application.Core;
using FacilityManager.Domain.Contracts;
using FacilityManager.Domain.Core.Results;
using FacilityManager.Domain.Equipments;
using FacilityManager.Domain.Facilities;
using FluentAssertions;
using Moq;

namespace FacilityManager.Tests.Application.ContactService;

public sealed class CreateAsync
{
    private readonly Mock<IContractRepository> _contractRepositoryMock;
    private readonly ContractService _contractService;
    private readonly ContractCreationDto _dto;
    private readonly Mock<IEquipmentRepository> _equipmentRepositoryMock;
    private readonly Mock<IFacilityRepository> _facilityRepositoryMock;

    public CreateAsync()
    {
        _facilityRepositoryMock = new Mock<IFacilityRepository>();
        _equipmentRepositoryMock = new Mock<IEquipmentRepository>();
        _contractRepositoryMock = new Mock<IContractRepository>();
        Mock<IUnitOfWork> unitOfWorkMock = new();

        var config = new MapperConfiguration(cfg => cfg.CreateMap<ContractCreationDto, Contract>());
        var mapper = config.CreateMapper();

        _contractService = new ContractService(
            mapper,
            unitOfWorkMock.Object,
            _facilityRepositoryMock.Object,
            _equipmentRepositoryMock.Object,
            _contractRepositoryMock.Object);

        _dto = new ContractCreationDto(
            Guid.NewGuid(),
            Guid.NewGuid(),
            1);
    }

    [Fact]
    public async Task ShouldReturnAmountError_WhenAmountIsZeroOrNegative()
    {
        var dto = _dto with { Amount = 0 };

        var result = await _contractService.CreateAsync(dto);

        result
            .Should()
            .BeEquivalentTo(Result.Failure(ContractErrors.AmountError));
    }

    [Fact]
    public async Task ShouldReturnFacilityNotFound_WhenFacilityDoesNotExist()
    {
        _facilityRepositoryMock.Setup(x =>
            x.GetAsync(_dto.FacilityCode)).ReturnsAsync((Facility)null!);

        var result = await _contractService.CreateAsync(_dto);

        result
            .Should()
            .BeEquivalentTo(Result.Failure(FacilityErrors.NotFound));
    }

    [Fact]
    public async Task ShouldReturnEquipmentNotFound_WhenEquipmentDoesNotExist()
    {
        _facilityRepositoryMock.Setup(x =>
            x.GetAsync(_dto.FacilityCode)).ReturnsAsync(new Facility());

        _equipmentRepositoryMock.Setup(x =>
            x.GetAsync(_dto.EquipmentCode)).ReturnsAsync((Equipment)null!);

        var result = await _contractService.CreateAsync(_dto);

        result
            .Should()
            .BeEquivalentTo(Result.Failure(EquipmentErrors.NotFound));
    }

    [Fact]
    public async Task ShouldReturnFacilityNotEnoughArea_WhenAreaExceedsFacilityLimit()
    {
        var dto = _dto with { Amount = 10 };
        var facility = new Facility { StandardArea = 1000f };
        var equipment = new Equipment { Area = 100f };
        var existingContracts = new List<Contract>
        {
            new() { FacilityCode = dto.FacilityCode, Amount = 5, Equipment = equipment }
        };

        _facilityRepositoryMock.Setup(x =>
            x.GetAsync(dto.FacilityCode)).ReturnsAsync(facility);

        _equipmentRepositoryMock.Setup(x =>
            x.GetAsync(dto.EquipmentCode)).ReturnsAsync(equipment);

        _contractRepositoryMock.Setup(x =>
            x.GetAllByFacilityCodeAsync(dto.FacilityCode, true)).ReturnsAsync(existingContracts);

        var result = await _contractService.CreateAsync(dto);

        result
            .Should()
            .BeEquivalentTo(Result.Failure(FacilityErrors.NotEnoughArea));
    }

    [Fact]
    public async Task ShouldReturnSuccess_WhenContractIsCreatedSuccessfully()
    {
        var dto = _dto with { Amount = 10 };
        var facility = new Facility { StandardArea = 1000f };
        var equipment = new Equipment { Area = 50f };
        var existingContracts = new List<Contract>();

        _facilityRepositoryMock.Setup(x =>
            x.GetAsync(dto.FacilityCode)).ReturnsAsync(facility);

        _equipmentRepositoryMock.Setup(x =>
            x.GetAsync(dto.EquipmentCode)).ReturnsAsync(equipment);

        _contractRepositoryMock.Setup(x =>
            x.GetAllByFacilityCodeAsync(dto.FacilityCode, true)).ReturnsAsync(existingContracts);

        var result = await _contractService.CreateAsync(dto);

        result
            .Should()
            .BeEquivalentTo(Result.Success());
    }

    [Fact]
    public async Task ShouldReturnCreateError_WhenExceptionOccursDuringContractCreation()
    {
        var dto = _dto with { Amount = 10 };
        var facility = new Facility { StandardArea = 1000f };
        var equipment = new Equipment { Area = 50f };
        var existingContracts = new List<Contract>();

        _facilityRepositoryMock.Setup(x =>
            x.GetAsync(dto.FacilityCode)).ReturnsAsync(facility);

        _equipmentRepositoryMock.Setup(x =>
            x.GetAsync(dto.EquipmentCode)).ReturnsAsync(equipment);

        _contractRepositoryMock.Setup(x =>
            x.GetAllByFacilityCodeAsync(dto.FacilityCode, true)).ReturnsAsync(existingContracts);

        _contractRepositoryMock.Setup(x =>
            x.AddAsync(It.IsAny<Contract>())).ThrowsAsync(new Exception("Database error"));

        var result = await _contractService.CreateAsync(dto);

        result
            .Should()
            .BeEquivalentTo(Result.Failure(ContractErrors.CreateError));
    }
}