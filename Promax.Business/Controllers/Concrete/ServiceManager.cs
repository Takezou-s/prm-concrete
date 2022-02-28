using Promax.Core;
using Promax.DataAccess;
using Promax.Entities;

namespace Promax.Business
{
    public class ServiceManager : Manager<Service>, IServiceManager
    {
        public ServiceManager(IEntityRepository<Service> repo) : base(repo)
        {

        }
        public ServiceManager(IBeeMapper mapper) : base(new RetentiveServiceRepository(new EntityRepository<ServiceDTO, ExpertContext>(), mapper))
        {

        }
    }
}
