using Promax.Core;
using Promax.DataAccess;
using Promax.Entities;

namespace Promax.Business
{
    public class NormViewManager : NonRetentiveManager<NormViewDTO, NormViewDTO>, INormViewManager
    {
        public NormViewManager(IComplexNonRetentiveEntityRepository<NormViewDTO, NormViewDTO> repo) : base(repo)
        {

        }
        public NormViewManager(IBeeMapper mapper) : base(new NonRetentiveNormViewRepository(new EntityRepository<NormViewDTO, ExpertContext>(), mapper))
        {

        }
    }
}
