using Promax.Core;
using Promax.Entities;

namespace Promax.DataAccess
{
    public class RetentiveSiloRepository : ComplexRetentiveEntityRepository<Silo, SiloDTO>, IRetentiveSiloRepository
    {
        public RetentiveSiloRepository(IEntityRepository<SiloDTO> database, IBeeMapper mapper) : base(database, nameof(SiloDTO.SiloId), mapper, null)
        {

        }
    }
}
