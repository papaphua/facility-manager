using FacilityManager.Domain.Equipments;
using FacilityManager.Persistence.Core;

namespace FacilityManager.Persistence.Equipments;

public sealed class EquipmentRepository(ApplicationDbContext db)
    : Repository<Equipment>(db), IEquipmentRepository;