using Promax.Core;
using Promax.Entities;

namespace Promax.DataAccess
{
    public class RetentiveServiceCategoryRepository : ComplexRetentiveEntityRepository<ServiceCategory, ServiceCategoryDTO>, IRetentiveServiceCategoryRepository
    {
        public RetentiveServiceCategoryRepository(IEntityRepository<ServiceCategoryDTO> database, IBeeMapper mapper) : base(database, nameof(ServiceCategoryDTO.CatId), mapper, null)
        {

        }
    }
}
