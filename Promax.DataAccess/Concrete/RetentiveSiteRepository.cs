using Promax.Core;
using Promax.Entities;

namespace Promax.DataAccess
{
    public class RetentiveSiteRepository : ComplexRetentiveEntityRepository<Site, SiteDTO>, IRetentiveSiteRepository
    {
        public RetentiveSiteRepository(IEntityRepository<SiteDTO> database, IBeeMapper mapper) : base(database, nameof(SiteDTO.SiteId), mapper, null)
        {

        }
    }
}
