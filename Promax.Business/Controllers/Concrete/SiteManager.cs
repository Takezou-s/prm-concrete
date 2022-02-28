using Promax.Core;
using Promax.DataAccess;
using Promax.Entities;

namespace Promax.Business
{
    public class SiteManager : Manager<Site>, ISiteManager
    {
        public SiteManager(IEntityRepository<Site> repo) : base(repo)
        {

        }
        public SiteManager(IBeeMapper mapper) : base(new RetentiveSiteRepository(new EntityRepository<SiteDTO, ExpertContext>(), mapper))
        {

        }
    }
}
