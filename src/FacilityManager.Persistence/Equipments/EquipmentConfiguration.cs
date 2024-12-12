using FacilityManager.Domain.Equipments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacilityManager.Persistence.Equipments;

public sealed class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        builder.HasKey(equipment => equipment.Code);

        builder.HasData(
            new Equipment
            {
                Code = Guid.NewGuid(),
                Name = "Excavator",
                Area = 120.5f
            },
            new Equipment
            {
                Code = Guid.NewGuid(),
                Name = "Bulldozer",
                Area = 98.0f
            },
            new Equipment
            {
                Code = Guid.NewGuid(),
                Name = "Hydraulic Hammer",
                Area = 25.3f
            },
            new Equipment
            {
                Code = Guid.NewGuid(),
                Name = "Concrete Mixer",
                Area = 35.0f
            },
            new Equipment
            {
                Code = Guid.NewGuid(),
                Name = "Tower Crane",
                Area = 300.0f
            },
            new Equipment
            {
                Code = Guid.NewGuid(),
                Name = "Forklift",
                Area = 18.7f
            },
            new Equipment
            {
                Code = Guid.NewGuid(),
                Name = "Backhoe Loader",
                Area = 75.2f
            },
            new Equipment
            {
                Code = Guid.NewGuid(),
                Name = "Road Roller",
                Area = 45.0f
            },
            new Equipment
            {
                Code = Guid.NewGuid(),
                Name = "Compactor",
                Area = 30.5f
            },
            new Equipment
            {
                Code = Guid.NewGuid(),
                Name = "Scissor Lift",
                Area = 22.8f
            }
        );
    }
}