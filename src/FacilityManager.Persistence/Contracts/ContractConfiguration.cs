using FacilityManager.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacilityManager.Persistence.Contracts;

public sealed class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(contract => contract.Id);

        builder
            .HasOne(contract => contract.Facility)
            .WithMany(facility => facility.Contracts)
            .HasForeignKey(contract => contract.FacilityCode);

        builder
            .HasOne(contract => contract.Equipment)
            .WithMany(equipment => equipment.Contracts)
            .HasForeignKey(contract => contract.EquipmentCode);
    }
}