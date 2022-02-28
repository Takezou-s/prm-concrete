using Promax.Core;
using Promax.DataAccess;
using Promax.Entities;

namespace Promax.Business
{
    public class ServiceCategoryManager : Manager<ServiceCategory>, IServiceCategoryManager
    {
        public ServiceCategoryManager(IEntityRepository<ServiceCategory> repo) : base(repo)
        {

        }
        public ServiceCategoryManager(IBeeMapper mapper) : base(new RetentiveServiceCategoryRepository(new EntityRepository<ServiceCategoryDTO, ExpertContext>(), mapper))
        {

        }
    }
}
