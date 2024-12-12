using FacilityManager.Domain.Contracts;
using FacilityManager.Persistence.Core;

namespace FacilityManager.Persistence.Contracts;

public sealed class ContractRepository(ApplicationDbContext db)
    : Repository<Contract>(db), IContractRepository;