using Promax.Core;
using Promax.Entities;

namespace Promax.DataAccess
{
    public class NonRetentiveNormViewRepository : ComplexNonRetentiveEntityRepository<NormViewDTO, NormViewDTO>, INonRetentiveNormViewRepository
    {
        public NonRetentiveNormViewRepository(IEntityRepository<NormViewDTO> database, IBeeMapper mapper) : base(database, mapper)
        {
        }
    }
}
