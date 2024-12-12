using FacilityManager.Domain.Facilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacilityManager.Persistence.Facilities;

public sealed class FacilityConfiguration : IEntityTypeConfiguration<Facility>
{
    public void Configure(EntityTypeBuilder<Facility> builder)
    {
        builder.HasKey(facility => facility.Code);

        builder.HasData(
            new Facility
            {
                Code = Guid.NewGuid(),
                Name = "Warehouse A",
                StandardArea = 500.0f
            },
            new Facility
            {
                Code = Guid.NewGuid(),
                Name = "Assembly Line B",
                StandardArea = 1200.0f
            },
            new Facility
            {
                Code = Guid.NewGuid(),
                Name = "Storage Unit C",
                StandardArea = 250.0f
            },
            new Facility
            {
                Code = Guid.NewGuid(),
                Name = "Research Lab D",
                StandardArea = 750.0f
            },
            new Facility
            {
                Code = Guid.NewGuid(),
                Name = "Data Center E",
                StandardArea = 1000.0f
            },
            new Facility
            {
                Code = Guid.NewGuid(),
                Name = "Testing Zone F",
                StandardArea = 350.0f
            },
            new Facility
            {
                Code = Guid.NewGuid(),
                Name = "Logistics Hub G",
                StandardArea = 800.0f
            },
            new Facility
            {
                Code = Guid.NewGuid(),
                Name = "Production Floor H",
                StandardArea = 1500.0f
            },
            new Facility
            {
                Code = Guid.NewGuid(),
                Name = "Control Room I",
                StandardArea = 200.0f
            },
            new Facility
            {
                Code = Guid.NewGuid(),
                Name = "Conference Hall J",
                StandardArea = 600.0f
            }
        );
    }
}