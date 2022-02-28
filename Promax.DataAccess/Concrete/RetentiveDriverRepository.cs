using Promax.Core;
using Promax.Entities;

namespace Promax.DataAccess
{
    public class RetentiveDriverRepository : ComplexRetentiveEntityRepository<Driver, DriverDTO>, IRetentiveDriverRepository
    {
        public RetentiveDriverRepository(IEntityRepository<DriverDTO> database, IBeeMapper mapper) : base(database, nameof(DriverDTO.DriverId), mapper, null)
        {

        }
    }
}
