using Promax.Core;
using Promax.Entities;

namespace Promax.DataAccess
{
    public class RetentiveServiceRepository : ComplexRetentiveEntityRepository<Service, ServiceDTO>, IRetentiveServiceRepository
    {
        public RetentiveServiceRepository(IEntityRepository<ServiceDTO> database, IBeeMapper mapper) : base(database, nameof(ServiceDTO.ServiceId), mapper, null)
        {

        }
    }
}
